using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;   //������ſ� �����带 ���� ����  TCP/Ip�� ������ ���Ӽ��� �����ϱ� ���� 



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
        //���Ͽ� ���� �� ������
        try 
        {
            _tcp_client = new TcpClient("localhost", 8053);    //localhost �� ��ǻ�� IP��Ī, 
            Byte[] bytes = new byte[1024];                      // 1024 ũ�⸸ŭ �޾Ƶ���, �޸� Ȯ��, �ʱ�ȭ�� �� �Ǿ null���� ������� 

            //1 ���´� -> 1�޾Ҵ� Ȯ��
            //1 ���´� -> (����) -> 2 ���´�  -> ������� �߻�

            while (true) 
            {
                using ( NetworkStream stream = _tcp_client.GetStream() )   //GetStream() �����Ͱ� �ö����� ��ٸ� 
                {
                    int length;
                    while ( (length = stream.Read(bytes, 0, bytes.Length)) != 0  )  //������ ���� ���� stream�̶� �� 
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
            if (stream.CanWrite)    //CanWrite ������ ������
            {
                byte[] client_mes = Encoding.ASCII.GetBytes(mes);       ///�޼����� ����Ʈ ������ �ٲ㼭 
                stream.Write(client_mes, 0, client_mes.Length);         ///�޼����� �޼������̸�ŭ ������
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
        if (client_thread.IsAlive) client_thread.Abort();  //tcp_thread�� ��������� ��������
        _tcp_client.Close();
    }


}
