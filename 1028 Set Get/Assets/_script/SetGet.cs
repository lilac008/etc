using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGet : MonoBehaviour
{
    private int _data;          /// ���ܳ������� ǥ���ϱ� ���� _�� ǥ��

    public int data 
    {
        get                         //���� ���� ���� �ܺη� �������� �Լ� 
        {                           
            Debug.Log("get ȣ��");
            return _data; 
        }

        set                         //�ܺ��� �Ű����� ���� ���� ������ ����    
        { 
            Debug.Log("set ȣ��");
            _data = value;          //value;�� data�� � �� ������� �� 
        }
    }

}
