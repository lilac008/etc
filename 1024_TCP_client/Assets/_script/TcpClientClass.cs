using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;   //소켓통신에 스레드를 쓰는 이유  TCP/Ip는 연결의 지속성을 유지하기 위해 



public class TcpClientClass : MonoBehaviour
{
    Thread client_thread;
    TcpClient _tcp_client;

    void Start()    
    {
        connect_tcp();
    }

    void Update()
    {
        //Debug.Log("Hi! Server~~~");

        if (Input.GetMouseButtonDown(0))
        {
            sendMes("Hi! Server~~~");
            //Debug.Log("Hi! Server~~~");
        }

    }




    void connect_tcp()
    {
        try 
        {
            client_thread = new Thread(new ThreadStart(listen_data));
            client_thread.IsBackground = true;
            client_thread.Start();
        } 
        catch(Exception e) 
        {  Debug.Log(e);  }

    }

    void listen_data() 
    {
        //소켓에 관한 걸 넣을듯
        try 
        {
            _tcp_client = new TcpClient("localhost", 8053);    //localhost 내 컴퓨터 IP지칭, 
            Byte[] bytes = new byte[1024];                      // 1024 크기만큼 받아들임, 메모리 확보, 초기화가 안 되어서 null값이 들어있음 

            //1 보냈다 -> 1받았다 확인
            //1 보냈다 -> (생략) -> 2 보냈다  -> 오버헤드 발생

            while (true) 
            {
                using ( NetworkStream stream = _tcp_client.GetStream() )   //GetStream() 데이터가 올때까지 기다림 
                {
                    int length;
                    while ( (length = stream.Read(bytes, 0, bytes.Length)) != 0  )  //데이터 받은 것을 stream이라 함 
                    {
                        var inData = new byte[length];
                        Array.Copy(bytes, 0, inData, 0, length);

                        string server_mes = Encoding.ASCII.GetString(inData);

                        Debug.Log(server_mes);
                    }
                }
            }
        } 
        catch(SocketException s) 
        {
            Debug.Log(s);
        }

    }


    void sendMes(string mes) 
    {
        if (_tcp_client == null)
        {
            Debug.Log("null");
            return;
        }
        
        try 
        {
            NetworkStream stream = _tcp_client.GetStream();
            if (stream.CanWrite)    //CanWrite 서버로 보낸다
            {
                byte[] client_mes = Encoding.ASCII.GetBytes(mes);       ///메세지를 바이트 단위로 바꿔서 
                stream.Write(client_mes, 0, client_mes.Length);         ///메세지를 메세지길이만큼 보내라
                Debug.Log("SEND OK:" + mes);
            }
        } 
        catch (SocketException s) 
        {
            Debug.Log(s);
        }

    }

    void OnDestroy()
    {
        if (client_thread.IsAlive) client_thread.Abort();  //tcp_thread가 살아있으면 강제종료
        _tcp_client.Close();
    }


}
