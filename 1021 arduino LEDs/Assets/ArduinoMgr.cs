using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;  //����  (Edit - ProjectSetting - Player - OtherSetting - Configuration - API Compatibility Level - .NET Framework�� �ؾ� ����)



/*
 
���� ��� (Network Socket, ��Ʈ��ũ ����) 

A  --socket-----------> B
   <-----------socket--  

- ��Ʈ��ũ ����� ���� �����͸� ��ȯ�ϴ� �������� �� socket�� ���� ���� ����(�����͸� ��ȯ�ϱ�� ����)�ϸ� �������� �ϴ� IP�ּ�, port��ȣ�� ����Ѵ�. 
                                                                                                                IP   �ּ� :  ex) ��ȭ��ȣ 
                                                                                                                PORT ��ȣ :  ex) ��ȯ��ȣ 

 */


public class ArduinoMgr : MonoBehaviour
{
    public enum PortNumber                              // ��Ʈ��ȣ ����                
    { 
        COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10, COM11, COM12, COM13, COM14, COM15, COM16
    }

    [SerializeField]
    private PortNumber portNumber = PortNumber.COM4;    // ��Ʈ��ȣ ���� 

    [SerializeField]
    private string baudRate = "9600";                   // ��żӵ� ����


    private SerialPort serialPort;                       // �������-��Ʈ ����

    
    private bool LED_R_State = false;                    
    private bool LED_B_State = false;
    private bool LED_Y_State = false;



    void Start()
    {
        serialPort = new SerialPort(portNumber.ToString(), int.Parse(baudRate));      // ������� ��Ʈ ���� - �ʱⰪ ����(PortNumber, BaudRate) => (PortNumber.COM4�� string��,  Baud Rate 9600�� int��)
    }

    void Update()
    {
    }



    public void LED_Toggle(string _input)               // Toggle Key : Ư�� Key �ϳ��� ON, OFF ����� �ϴ� ��
    {
        bool LED_State = false;                         // LED_State �ʱⰪ F

        switch (_input)                                /// LED ���°� �ҷ�����???????
        {
            case "R":                                  // �Ű������� R�̸� LED_State�� LED_R_State ����
                LED_State = LED_R_State;
                break;
            case "B":
                LED_State = LED_B_State;
                break;
            case "Y":
                LED_State = LED_Y_State;
                break;
        }

        if (!serialPort.IsOpen)                             // ��������� ������ �ȵǾ������� ����
            serialPort.Open();                              

        if (LED_State == false)                             // LED ���°� �ҵ��̸� 
        {
            serialPort.Write(_input);                       // ���� ��Ʈ�� �Ű����� �� ���
            LED_State = !LED_State;
        }
        else if (LED_State)                                 // LED ���°� �����̸�
        {
            serialPort.Write(_input.ToLower());            // ���� ��Ʈ�� �Ű����� �ҹ��ڰ� ���
            LED_State = !LED_State;
        }

        switch (_input)                                /// LED ���°� ����???????
        {
            case "R":
                LED_R_State = LED_State;               //�Ű������� R�̸� LED_R_State�� LED_State ����
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
