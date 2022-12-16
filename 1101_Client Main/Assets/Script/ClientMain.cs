using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class ClientMain : MonoBehaviour
{
    string m_ip = "192.168.10.72";                          //���2 cmd â���� ipconfig
    int m_port = 45402;
    TcpClient m_Client;
    Thread m_thr_recv;
    Queue<string> m_queue = new Queue<string>();            //�ڷᱸ�� 07. ����ť - �ܺο��� �����Ͱ� ������ ���ʴ�� ���ٰ� 9��° ���� �� ���� ó������ �����Ͱ� ���� / ������ ������ ���۰� ���� ����Ǿ� ����
                                                            // stack ���Լ���, string���·� queue�޸� ���¸� ��� �־��ٰ� �� ��
    public Text m_meswin;



    void Start()
    {
        m_meswin.text = "";                                //�ƹ��͵� ������ �޸𸮴� Ȯ���� ����
        connect_to_server();
        
        //Debug.Log(m_Client.Connected);
    }

    void Update()
    {
        Que_mesout();

        if (Input.GetMouseButtonDown(0))
        {
            SendData("���콺�� Ŭ���Ͽ����ϴ�.");
        }
    }








    void connect_to_server()
    {
        try
        {
            m_thr_recv = new Thread(new ThreadStart(client_recv));                      //client�� Thread�ϳ��� ����
            m_thr_recv.IsBackground = true;
            m_thr_recv.Start();
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    void client_recv()
    {
        try
        {
            m_Client = new TcpClient(m_ip, m_port);
            Byte[] bytes = new Byte[1024];

            using( NetworkStream stream = m_Client.GetStream() )
            {
                int len;

                while( (len = stream.Read(bytes,0, bytes.Length)) != 0 )                // bytes�� ó������ ���̸�ŭ �о��
                {
                    var com_data = new Byte[len];
                    Array.Copy( bytes, 0, com_data, 0, len );                           // bytes�� ó������ com_data ó������ ���̸�ŭ ??????

                    string mes = Encoding.Default.GetString( com_data );

                    m_queue.Enqueue( mes );
                    //Debug.Log(mes);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SendData(string mes)
    {
        if (m_Client == null) return;

        try
        {
            NetworkStream stream = m_Client.GetStream();
            if( stream.CanWrite )
            {
                byte[] send_data = Encoding.Default.GetBytes(mes);
                stream.Write(send_data, 0, send_data.Length);
            }

        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }



    void OnApplicationQuit()                                                //thread�� ���������
    {
        m_thr_recv.Abort();

        if( m_Client!=null )
        {
            m_Client.Close();
            m_Client=null;
        }
    }

    void Que_mesout()
    {
        if (m_queue.Count>0)
        {
            if (m_meswin.text.Length==0)
            {
                m_meswin.text = m_queue.Dequeue();
            }
            else
            {
                m_meswin.text += "\n" + m_queue.Dequeue();
            }
        }
    }


}
