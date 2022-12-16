using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Arduino : MonoBehaviour
{
    // SerialPort serialPort = new SerialPort(string portName(포트넘버), int baudRate(통신속도), Parity parity, int dataBits, StopBits stopBits);
       SerialPort SerialPort1 = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);


    void Start()
    {
        SerialPort1.Open();     //직렬통신 포트 연결
    }

    void Update()
    {
        switch (Input.inputString)      ///입력 받은 문자열에 따라 직렬통신 포트에 문자열로 송신
        {
            case "A":
            case "a":
                Debug.Log("press A");
                SerialPort1.WriteLine("A");
                break;
            case "S":
            case "s":
                Debug.Log("press S");
                SerialPort1.WriteLine("S");
                break;
            case "D":
            case "d":
                Debug.Log("press D");
                SerialPort1.WriteLine("D");
                break;
            case "F":
            case "f":
                Debug.Log("press F");
                SerialPort1.WriteLine("F");
                break;
            case "G":
            case "g":
                Debug.Log("press G");
                SerialPort1.WriteLine("G");
                break;
            case "H":
            case "h":
                Debug.Log("press G");
                SerialPort1.WriteLine("H");
                break;
            case "J":
            case "j":
                Debug.Log("press J");
                SerialPort1.WriteLine("J");
                break;
            case "K":
            case "k":
                Debug.Log("press K");
                SerialPort1.WriteLine("K");
                break;
        }
    }


    void OnApplicationQuit()    /// 앱이 종료되면 직렬 통신도 종료
    {
        SerialPort1.Close();
    }
}
