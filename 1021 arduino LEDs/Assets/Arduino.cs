using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Arduino : MonoBehaviour
{
    // SerialPort serialPort = new SerialPort(string portName(��Ʈ�ѹ�), int baudRate(��żӵ�), Parity parity, int dataBits, StopBits stopBits);
       SerialPort SerialPort1 = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);


    void Start()
    {
        SerialPort1.Open();     //������� ��Ʈ ����
    }

    void Update()
    {
        switch (Input.inputString)      ///�Է� ���� ���ڿ��� ���� ������� ��Ʈ�� ���ڿ��� �۽�
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


    void OnApplicationQuit()    /// ���� ����Ǹ� ���� ��ŵ� ����
    {
        SerialPort1.Close();
    }
}
