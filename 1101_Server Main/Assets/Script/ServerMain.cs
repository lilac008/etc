using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;



/* TCP (Transmission Control Protocol)
- �� ���ͳݿ��� ���Ǵ� ��� ���������� ǥ������ ���� ������ �ٸ� ��ǻ�� �ý��� ���� ������ ������ ���� ���ߵ� ��� ��������
- ���� �� ���� ���� -> ���ͳ�(World Wide Web), ���Ӽ���

- 1) ������ Socket. ����� ���.  
- 2) �߽����� �������� �����Ͽ� packet�� �����ϱ� ���� ���� ��θ� ���� (data�� packet�������� ������ ������ �°� �޾Ҵ��� ȣ���ؼ� Ȯ��) 
- 3) �۽ŵ� ������ ���� �ߺ����� �ʰ� ������ ���� 
- 4) ������ �ŷڼ�, ��Ȯ�� ������ UDP�� ���� �ӵ��� ���� (CPU �Ҹ� ��) 
- 5) overhead(ó���ϴµ� ���� �ð�, �޸� �ʰ��� ��) �߻� : ������ ���� �ߺ����� �ʵ��� protocol�� �������� protocol�� �� ���� �� �̻� �� ������ �����°� ��    ex) ��Ʃ�� ȭ��ī�޶� ȭ����� / ������� = ������ = ȭ�����) 
- 6) ������ ũ�� ������

 */


/// server Main �ʴ� 60������ - ���� ��ſ��� �ʴ� 60�������� ��ǻ�� �󿡼��� ������ �� �ð��̹Ƿ� ������ ������ �ﰢ �����ϱ� ���� �ܺο� thread ���� 
/// Thread - listener () : �ܺο��� �����Ͱ� �����ߴ��� �ƴ��� �����, �����Ͱ� ������ �׋� ����

//client���� 1:1�� Thread ����


public class ServerMain : MonoBehaviour
{
    public string m_ip;                                             /// m = membership
    int m_port;         
    public Text m_ip_port;
    public Text m_con_counter;
    public Text m_server_mes;

    Thread m_thr_listener;
    TcpListener m_tcp_listener;                                     //  server      : client�� ���� ��û�� ��ٸ��� ����
    TcpClient m_Client;                                             //  client      : server�� ���� ��û

    List<TcpClient> m_clients_list = new List<TcpClient>( new TcpClient[0] );

    Queue<string> m_server_mes_que = new Queue<string>();



    void Start()
    {
        m_ip = IPManager.GetIP( IPManager.ADDRESSFAM.IPv4 );
        m_port = 45402;

        m_ip_port.text = "IP:" + m_ip + " PORT:" + m_port;

        m_thr_listener = new Thread(new ThreadStart(thr_listener));
        m_thr_listener.IsBackground = true;
        m_thr_listener.Start();
    }

    void Update()
    {
        m_con_counter.text = "Connected : " + get_client_counter();
        server_mes_out();
    }











    void thr_listener()
    {
        m_tcp_listener = new TcpListener( IPAddress.Parse(m_ip), m_port );
        m_tcp_listener.Start();
        ThreadPool.QueueUserWorkItem(listerner, null);
    }

    void listerner(object token)                                            //token�� m_Client�� ����
    {
        while (m_tcp_listener != null)
        {
            m_Client = m_tcp_listener.AcceptTcpClient();

            ///////////////////////////
            Socket c = m_Client.Client;
            IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
            //IPEndPoint ip_point = c.RemoteEndPoint as IPEndPoint;
            string ip = ip_point.Address.ToString();

            m_server_mes_que.Enqueue( "Connected : " + ip );

            //m_server_mes.text = "Connected : " + ip;
            //Debug.Log("Connected : " + ip);
            ///////////////////////////

            m_clients_list.Add(m_Client);

            SendData( m_Client, "������ �����Ͽ����ϴ�." );


            ThreadPool.QueueUserWorkItem(client_worker, m_Client);
        }
    }

    void client_worker(object token)                        //token�� m_Client�� ����
    {
        Byte[] bytes = new byte[1024];                      //���� 1024
    
        using( var client = token as TcpClient )            //using (�ȿ� �� ����) - �޸� Ȯ���ߴٰ� �����ϰ� �����ϱ� ���� ���   /  token�� TcpClient������ ���� 
        using( var stream = client.GetStream() )            
        {
            int len;
            while( (len = stream.Read(bytes, 0, bytes.Length))!=0 )
            {
                var com_data = new byte[len];                               //�� ���� 1024 �߿� ���� ������ n�� Ȯ��
                Array.Copy(bytes, 0, com_data, 0, len);                     //len ��ŭ �����϶�.  Array.Copy�� byte

                string client_mes = Encoding.Default.GetString(com_data);
                Send_data_all(client_mes);
                //Debug.Log(client_mes)
            }
            Socket c = client.Client;
            IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
            string ip = ip_point.ToString();

            m_server_mes_que.Enqueue("��������:" + ip);
        }
    }

    void Send_data_all(string mes)                          //��ο��� ������ ����
    {
        for (int i = 0; i < m_clients_list.Count; i++)
        {
            if (!m_clients_list[i].Connected)
            {
                m_clients_list.RemoveAt(i);
            }
            else
            {
                SendData(m_clients_list[i], mes);
            }
        }
        
    }

    void SendData( object token, string mes )                               ///1���� ������ ����
    {
        var client = token as TcpClient;                                    //�ڷ������� ����
        try
        {
            NetworkStream stream = client.GetStream();
            if( stream.CanWrite )
            {
                byte[] send_data = Encoding.Default.GetBytes(mes);
                stream.Write(send_data, 0, send_data.Length);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    int get_client_counter()
    {
        for( int i=0; i<m_clients_list.Count; i++ )
        {
            if( !m_clients_list[i].Connected )
            {
                m_clients_list.RemoveAt(i);
            }
        }
        return m_clients_list.Count;
    }

    void server_mes_out()
    {
        if( m_server_mes_que.Count>0)                                       // m_server_mes_que.Count�� ������
        {
            m_server_mes.text += m_server_mes_que.Dequeue() + "\n";         //Enqueue �����͸� �ֱ�, Dequeue �����͸� ��������
        }
    }





    void OnApplicationQuit()    //��������
    {
        m_thr_listener.Abort();
        if (m_tcp_listener != null)
        {
            m_tcp_listener.Stop();
            m_tcp_listener = null;
        }
    }

}
