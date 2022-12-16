using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;



public class TcpServer : MonoBehaviour
{
    TcpListener tcp_listener;        //외부에서 유저(client)가 접속할때 받는 함수
    Thread tcp_thread;
    TcpClient tcp_client;           //유저(client) 임시로 1명, 만일 그 이상하고 싶으면 배열로 잡아서 index순서대로 넣으면 됨


    void Start()
    {
        make_thread();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            sendMes("Hi! Client~~~");
        }

    }




    void make_thread() 
    {
        tcp_thread = new Thread( new ThreadStart(tcp_thread_process));
        tcp_thread.IsBackground = true;
        tcp_thread.Start();
    }

    void tcp_thread_process() //받아오는거 
    {
        try 
        {
            tcp_listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8053);
            tcp_listener.Start();
            Byte[] bytes = new byte[1024];      //바이트가 데이터를 access하는데 굉장히 빠름, string은 직접적으로 쓰기에 cpu리소스를 많이 잡아먹음

            while (true) 
            {
                using (tcp_client = tcp_listener.AcceptTcpClient()) // 유저정보를 받아서 tcpClient 저장,  유령 클라이언트를 없애기 위해 주말마다 껐다킴, 지하철같은경우udp로 강제로 밀어버리거나 그때그때 tcp/IP로 연결했다가 종료하고 연결했다가 종료하고 이러면 서버가 죽어난다. 시간이 약간 걸린다. 서버는 속도도 빨라야 하고 메모리 양도 많아야하고 cpu성능도 좋아야 한다. 
                {
                    Debug.Log("login");
                    using (NetworkStream stream = tcp_client.GetStream())   //stream을 받아와서 대기상태
                    {
                        int length;
                        while ( (length = stream.Read(bytes, 0, bytes.Length)) != 0 )
                        {
                            var comData = new byte[length];
                            Array.Copy(bytes, 0, comData, 0, length);

                            string mes = Encoding.ASCII.GetString(comData);

                            Debug.Log(mes);
                        }
                    }
                }
            }
        } 
        catch (Exception s) 
        { Debug.Log(s); }
    }


    //서버에서 클라이언트로 메세지 보내기
    void sendMes(string mes) 
    {
        if (tcp_client == null) return;

        try 
        {
            NetworkStream stream = tcp_client.GetStream();
            if (stream.CanWrite) //스트림에서 write할 준비가 되면
            {
                byte[] send_mes = Encoding.ASCII.GetBytes(mes);
                stream.Write(send_mes, 0, send_mes.Length);
            }
        } 
        catch 
        (SocketException s)
        {
            Debug.Log(s);
        }
    }


    void OnDestroy()
    {
        if(tcp_thread.IsAlive) tcp_thread.Abort();  //tcp_thread가 살아있으면 스레드 강제종료
        tcp_client.Close();                         //유저 종료
    }


}
