using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;         /// �����带 


public class UdpReceiver : MonoBehaviour
{
    Thread recvThread;                    
    UdpClient client;              
    public int port;                  //��Ʈ ����
    public string recvUDPPacket;      //�������ӽ��������?

    void Start()
    {
        init();                 //�ʱⰪ ����
    }

    void Update() // 60���� 1�ʸ��� 
    {
    }



    void init()
    {
        port = 8051;    //6���� �߿� 8051�� ��Ʈ�� ����

        recvThread = new Thread(new ThreadStart(RecvData));
        recvThread.Start();
    }


    void RecvData() 
    {
        ///127.0.0.1  - ���ܺη� ���� �� �ִ� ��ȣ / ��Ʈ�� �� ��ǻ�� �ȿ� �����ִ� ����Ʈ���� , �����ǿ� ��Ʈ�� ����Ǿ�����
        client = new UdpClient(port);   ///���ε��Ǿ��ִ�?
        
        while (true) 
        {
            try
            {
                //IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port); ���� ���� �ִ� �����Ҽ� �ִ� IP�������� . 127.0.0.1�̸� 127.0.0.??���� ��� �� �޾Ƶ���. ����ǻ�Ϳ� �ִ� ��� ��Ʈ������ �ּҸ� �� �޾Ƶ��δٴ� �ǹ�. 
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);        // ���ε��Ǿ��ִ� Ŭ���̾�Ʈ.�� �޾ƶ�(������ �ּ�)
                string test = Encoding.UTF8.GetString(data);    // ���¸� �ٲ���.UTF8 �����ڵ� ���·�
                Debug.Log(test);                                //ȭ�鿡 ����϶�. 


                recvUDPPacket = test;

            }
            catch (Exception e)  
            {
                Debug.Log(e);   //�����߻��� �����޼��� Ȯ���ض�. try-catch�� ����ϸ� ���� �߻��� ���� �ٿ��� �ȵ�, ���� �ַ� ��Ʈ�p�̳� ����IO(in-out)���� ����Ѵ�.
            }
            //��ǻ�� ���� - 0.5�ʴ� �� ������ 1�� ������ ��ǻ�Ͱ� ������.
           
        }

    }


    void OnGUI()    //�Լ��������� ���� �������� ó��, ����׿�,
    {
        Rect recObj = new Rect(40, 10, 200, 400);   ///��ġ, 200, 400 ������ �ڽ� �ȿ�
        GUIStyle style = new GUIStyle();        ///�׷��������������̽� ����·� �������� ����, 
        style.alignment = TextAnchor.UpperLeft;     //�ؽ�Ʈ��Ŀ .������

        GUI.Box(recObj, "Packet : " + recvUDPPacket, style );
    }

    void OnDestroy()
    {
        if(recvThread.IsAlive)  recvThread.Abort(); //�����尡 ��������� �����϶�?
        client.Close();
    }


}

