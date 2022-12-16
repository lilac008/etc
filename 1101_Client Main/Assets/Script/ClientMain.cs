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
    string m_ip = "192.168.10.72";                          //방법2 cmd 창에서 ipconfig
    int m_port = 45402;
    TcpClient m_Client;
    Thread m_thr_recv;
    Queue<string> m_queue = new Queue<string>();            //자료구조 07. 원형큐 - 외부에서 데이터가 들어오면 차례대로 들어가다가 9번째 됐을 때 제일 처음가서 데이터가 들어옴 / 데이터 구조가 시작과 끝이 연결되어 있음
                                                            // stack 선입선출, string형태로 queue메모리 형태를 잡고 넣었다가 빼 씀
    public Text m_meswin;



    void Start()
    {
        m_meswin.text = "";                                //아무것도 없지만 메모리는 확보된 상태
        connect_to_server();
        
        //Debug.Log(m_Client.Connected);
    }

    void Update()
    {
        Que_mesout();

        if (Input.GetMouseButtonDown(0))
        {
            SendData("마우스를 클릭하였습니다.");
        }
    }








    void connect_to_server()
    {
        try
        {
            m_thr_recv = new Thread(new ThreadStart(client_recv));                      //client에 Thread하나만 생성
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

                while( (len = stream.Read(bytes,0, bytes.Length)) != 0 )                // bytes를 처음부터 길이만큼 읽어옴
                {
                    var com_data = new Byte[len];
                    Array.Copy( bytes, 0, com_data, 0, len );                           // bytes를 처음부터 com_data 처음에서 길이만큼 ??????

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



    void OnApplicationQuit()                                                //thread를 없애줘야함
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
