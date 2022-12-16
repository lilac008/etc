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
    TcpListener tcp_listener;        //�ܺο��� ����(client)�� �����Ҷ� �޴� �Լ�
    Thread tcp_thread;
    TcpClient tcp_client;           //����(client) �ӽ÷� 1��, ���� �� �̻��ϰ� ������ �迭�� ��Ƽ� index������� ������ ��


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

    void tcp_thread_process() //�޾ƿ��°� 
    {
        try 
        {
            tcp_listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8053);
            tcp_listener.Start();
            Byte[] bytes = new byte[1024];      //����Ʈ�� �����͸� access�ϴµ� ������ ����, string�� ���������� ���⿡ cpu���ҽ��� ���� ��Ƹ���

            while (true) 
            {
                using (tcp_client = tcp_listener.AcceptTcpClient()) // ���������� �޾Ƽ� tcpClient ����,  ���� Ŭ���̾�Ʈ�� ���ֱ� ���� �ָ����� ����Ŵ, ����ö�������udp�� ������ �о�����ų� �׶��׶� tcp/IP�� �����ߴٰ� �����ϰ� �����ߴٰ� �����ϰ� �̷��� ������ �׾��. �ð��� �ణ �ɸ���. ������ �ӵ��� ����� �ϰ� �޸� �絵 ���ƾ��ϰ� cpu���ɵ� ���ƾ� �Ѵ�. 
                {
                    Debug.Log("login");
                    using (NetworkStream stream = tcp_client.GetStream())   //stream�� �޾ƿͼ� ������
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


    //�������� Ŭ���̾�Ʈ�� �޼��� ������
    void sendMes(string mes) 
    {
        if (tcp_client == null) return;

        try 
        {
            NetworkStream stream = tcp_client.GetStream();
            if (stream.CanWrite) //��Ʈ������ write�� �غ� �Ǹ�
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
        if(tcp_thread.IsAlive) tcp_thread.Abort();  //tcp_thread�� ��������� ������ ��������
        tcp_client.Close();                         //���� ����
    }


}
