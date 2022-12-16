using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;         /// 스레드를 


public class UdpReceiver : MonoBehaviour
{
    Thread recvThread;                    
    UdpClient client;              
    public int port;                  //포트 선언
    public string recvUDPPacket;      //데이터임시저장장소?

    void Start()
    {
        init();                 //초기값 배정
    }

    void Update() // 60분의 1초마다 
    {
    }



    void init()
    {
        port = 8051;    //6만개 중에 8051번 포트로 지정

        recvThread = new Thread(new ThreadStart(RecvData));
        recvThread.Start();
    }


    void RecvData() 
    {
        ///127.0.0.1  - ㄴ외부로 나갈 수 있는 번호 / 포트는 내 컴퓨터 안에 열어주는 게이트같은 , 아이피에 포트가 연결되어있음
        client = new UdpClient(port);   ///바인딩되어있는?
        
        while (true) 
        {
            try
            {
                //IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port); 현재 내가 최대 수용할수 있는 IP가져오기 . 127.0.0.1이면 127.0.0.??까지 모두 다 받아들임. 내컴퓨터에 있는 모든 포트아이피 주소를 다 받아들인다는 의미. 
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);        // 바인딩되어있는 클라이언트.를 받아라(참조형 주소)
                string test = Encoding.UTF8.GetString(data);    // 형태를 바꿔줌.UTF8 유니코드 형태로
                Debug.Log(test);                                //화면에 출력하라. 


                recvUDPPacket = test;

            }
            catch (Exception e)  
            {
                Debug.Log(e);   //에러발생시 에러메세지 확인해라. try-catch를 사용하면 에러 발생시 게임 다운이 안됨, 따라서 주로 네트웤이나 파일IO(in-out)때만 사용한다.
            }
            //컴퓨터 전기 - 0.5초는 안 꺼지고 1초 꺼지면 컴퓨터가 꺼진다.
           
        }

    }


    void OnGUI()    //함수구조에서 가장 마지막에 처리, 디버그용,
    {
        Rect recObj = new Rect(40, 10, 200, 400);   ///위치, 200, 400 영역인 박스 안에
        GUIStyle style = new GUIStyle();        ///그래픽유저인터페이스 어떤형태로 찍을꺼냐 색상, 
        style.alignment = TextAnchor.UpperLeft;     //텍스트앵커 .위좌측

        GUI.Box(recObj, "Packet : " + recvUDPPacket, style );
    }

    void OnDestroy()
    {
        if(recvThread.IsAlive)  recvThread.Abort(); //스레드가 살아있으면 종료하라?
        client.Close();
    }


}

