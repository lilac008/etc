using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGet_first : MonoBehaviour
{
    int data; ///private int data; �� �����Ǿ� ���� 

    public int Get()            //���� ���� ���� �ܺη� �������� �Լ� 
    {
        return data;         
    }

    public void Set(int _data)  //�ܺ��� �Ű����� ���� ���� ������ ����
    {
        data = _data;
    }

}
