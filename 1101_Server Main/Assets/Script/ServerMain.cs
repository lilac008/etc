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
- 현 인터넷에서 사용되는 통신 프로토콜의 표준으로 서로 기종이 다른 컴퓨터 시스템 간의 데이터 전송을 위해 개발된 통신 프로토콜
- 군사 및 연구 목적 -> 인터넷(World Wide Web), 게임서버

- 1) 연결형 Socket. 양방향 통신.  
- 2) 발신지와 수신지를 연결하여 packet을 전송하기 위한 논리적 경로를 배정 (data를 packet조각으로 나누고 순서에 맞게 받았는지 호출해서 확인) 
- 3) 송신된 순서에 따라 중복되지 않게 데이터 수신 
- 4) 데이터 신뢰성, 정확도 높으나 UDP에 비해 속도가 느림 (CPU 소모 등) 
- 5) overhead(처리하는데 들어가는 시간, 메모리 초과된 것) 발생 : 순서에 따라 중복되지 않도록 protocol로 만들어놓고 protocol이 꽉 차면 더 이상 못 보내고 대기상태가 됨    ex) 유튜브 화상카메라 화면깨짐 / 오버헤드 = 대기상태 = 화면깨짐) 
- 6) 데이터 크기 무제한

 */


/// server Main 초당 60프레임 - 소켓 통신에서 초당 60프레임은 컴퓨터 상에서는 굉장히 긴 시간이므로 유저가 들어오면 즉각 반응하기 위해 외부에 thread 생성 
/// Thread - listener () : 외부에서 데이터가 접속했는지 아닌지 대기중, 데이터가 들어오면 그떄 새성

//client마다 1:1로 Thread 생성


public class ServerMain : MonoBehaviour
{
    public string m_ip;                                             /// m = membership
    int m_port;         
    public Text m_ip_port;
    public Text m_con_counter;
    public Text m_server_mes;

    Thread m_thr_listener;
    TcpListener m_tcp_listener;                                     //  server      : client의 연결 요청을 기다리는 역할
    TcpClient m_Client;                                             //  client      : server에 연결 요청

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

    void listerner(object token)                                            //token에 m_Client가 들어옴
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

            SendData( m_Client, "서버에 접속하였습니다." );


            ThreadPool.QueueUserWorkItem(client_worker, m_Client);
        }
    }

    void client_worker(object token)                        //token에 m_Client가 들어옴
    {
        Byte[] bytes = new byte[1024];                      //버퍼 1024
    
        using( var client = token as TcpClient )            //using (안에 든 내용) - 메모리 확보했다가 안전하게 해제하기 위해 사용   /  token을 TcpClient형으로 변형 
        using( var stream = client.GetStream() )            
        {
            int len;
            while( (len = stream.Read(bytes, 0, bytes.Length))!=0 )
            {
                var com_data = new byte[len];                               //총 버퍼 1024 중에 받은 데이터 n개 확보
                Array.Copy(bytes, 0, com_data, 0, len);                     //len 만큼 복사하라.  Array.Copy는 byte

                string client_mes = Encoding.Default.GetString(com_data);
                Send_data_all(client_mes);
                //Debug.Log(client_mes)
            }
            Socket c = client.Client;
            IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
            string ip = ip_point.ToString();

            m_server_mes_que.Enqueue("연결종료:" + ip);
        }
    }

    void Send_data_all(string mes)                          //모두에게 데이터 보냄
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

    void SendData( object token, string mes )                               ///1명에게 데이터 보냄
    {
        var client = token as TcpClient;                                    //자료형으로 변형
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
        if( m_server_mes_que.Count>0)                                       // m_server_mes_que.Count가 있으면
        {
            m_server_mes.text += m_server_mes_que.Dequeue() + "\n";         //Enqueue 데이터를 넣기, Dequeue 데이터를 가져오기
        }
    }





    void OnApplicationQuit()    //강제종료
    {
        m_thr_listener.Abort();
        if (m_tcp_listener != null)
        {
            m_tcp_listener.Stop();
            m_tcp_listener = null;
        }
    }

}
