using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;  //선언  (Edit - ProjectSetting - Player - OtherSetting - Configuration - API Compatibility Level - .NET Framework를 해야 적용)



/*
 
소켓 통신 (Network Socket, 네트워크 소켓) 

A  --socket-----------> B
   <-----------socket--  

- 네트워크 통신을 위해 데이터를 교환하는 지점으로 이 socket을 통해 서로 응답(데이터를 교환하기로 합의)하며 보내고자 하는 IP주소, port번호를 명시한다. 
                                                                                                                IP   주소 :  ex) 전화번호 
                                                                                                                PORT 번호 :  ex) 교환번호 

 */


public class ArduinoMgr : MonoBehaviour
{
    public enum PortNumber                              // 포트번호 열거                
    { 
        COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10, COM11, COM12, COM13, COM14, COM15, COM16
    }

    [SerializeField]
    private PortNumber portNumber = PortNumber.COM4;    // 포트번호 선언 

    [SerializeField]
    private string baudRate = "9600";                   // 통신속도 설정


    private SerialPort serialPort;                       // 직렬통신-포트 열기

    
    private bool LED_R_State = false;                    
    private bool LED_B_State = false;
    private bool LED_Y_State = false;



    void Start()
    {
        serialPort = new SerialPort(portNumber.ToString(), int.Parse(baudRate));      // 직렬통신 포트 열기 - 초기값 설정(PortNumber, BaudRate) => (PortNumber.COM4는 string형,  Baud Rate 9600은 int형)
    }

    void Update()
    {
    }



    public void LED_Toggle(string _input)               // Toggle Key : 특정 Key 하나로 ON, OFF 기능을 하는 것
    {
        bool LED_State = false;                         // LED_State 초기값 F

        switch (_input)                                /// LED 상태값 불러오기???????
        {
            case "R":                                  // 매개변수가 R이면 LED_State에 LED_R_State 저장
                LED_State = LED_R_State;
                break;
            case "B":
                LED_State = LED_B_State;
                break;
            case "Y":
                LED_State = LED_Y_State;
                break;
        }

        if (!serialPort.IsOpen)                             // 직렬통신이 연결이 안되어있으면 열기
            serialPort.Open();                              

        if (LED_State == false)                             // LED 상태가 소등이면 
        {
            serialPort.Write(_input);                       // 직렬 포트에 매개변수 값 출력
            LED_State = !LED_State;
        }
        else if (LED_State)                                 // LED 상태가 점등이면
        {
            serialPort.Write(_input.ToLower());            // 직렬 포트에 매개변수 소문자값 출력
            LED_State = !LED_State;
        }

        switch (_input)                                /// LED 상태값 저장???????
        {
            case "R":
                LED_R_State = LED_State;               //매개변수가 R이면 LED_R_State에 LED_State 저장
                break;
            case "B":
                LED_B_State = LED_State;
                break;
            case "Y":
                LED_Y_State = LED_State;
                break;
        }

    }
}
