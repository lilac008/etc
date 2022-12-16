using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class UdpSender : MonoBehaviour // UdpSender클래스
{
    int port;                   //포트 선언
    string IP;                  //ip 선언
    IPEndPoint remoteEndPoint;  //모든 아이피 다 끌어오기
    UdpClient client;
    string message;


    void Start()
    {
        init();
    }

    void Update()   //업데이트시 아래 메세지를 보냄
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            sendString("안녕하세요");
        }
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            sendString("Q를 눌렀습니다.");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            sendString("E를 눌렀습니다.");
        }
    }


    void init()
    {
        IP = "127.0.0.1";    //ip는 모든 컴퓨터 아이피
        port = 8051;         //port는 8051을 열어서 데이터 전송하겠다
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);     // ip에서 포트를 연다. 
        client = new UdpClient();
    }

    void sendString(string _message)    // _message = UTF8 -  
    {
        try 
        {
            //에러가 발생하면 실행시키지말고 그대로 진행하라 
            byte[] data = Encoding.UTF8.GetBytes(_message); // .stringg형으로 되어있는 UTF8 코드를 byte 형태로 바꾸어라.

            client.Send(data, data.Length, remoteEndPoint);
        } 
        catch (Exception e) 
        {
            Debug.Log(e);   //에러 발생하고 실행이 안되면 출력하라.
        }


    }


    void OnDestroy()
    {
        client.Close(); //클라이언트를 종료
    }




}
