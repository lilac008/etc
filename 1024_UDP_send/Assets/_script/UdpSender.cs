using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class UdpSender : MonoBehaviour // UdpSenderŬ����
{
    int port;                   //��Ʈ ����
    string IP;                  //ip ����
    IPEndPoint remoteEndPoint;  //��� ������ �� �������
    UdpClient client;
    string message;


    void Start()
    {
        init();
    }

    void Update()   //������Ʈ�� �Ʒ� �޼����� ����
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            sendString("�ȳ��ϼ���");
        }
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            sendString("Q�� �������ϴ�.");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            sendString("E�� �������ϴ�.");
        }
    }


    void init()
    {
        IP = "127.0.0.1";    //ip�� ��� ��ǻ�� ������
        port = 8051;         //port�� 8051�� ��� ������ �����ϰڴ�
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);     // ip���� ��Ʈ�� ����. 
        client = new UdpClient();
    }

    void sendString(string _message)    // _message = UTF8 -  
    {
        try 
        {
            //������ �߻��ϸ� �����Ű������ �״�� �����϶� 
            byte[] data = Encoding.UTF8.GetBytes(_message); // .stringg������ �Ǿ��ִ� UTF8 �ڵ带 byte ���·� �ٲپ��.

            client.Send(data, data.Length, remoteEndPoint);
        } 
        catch (Exception e) 
        {
            Debug.Log(e);   //���� �߻��ϰ� ������ �ȵǸ� ����϶�.
        }


    }


    void OnDestroy()
    {
        client.Close(); //Ŭ���̾�Ʈ�� ����
    }




}
