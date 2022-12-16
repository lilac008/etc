using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGet_first : MonoBehaviour
{
    int data; ///private int data; 로 생략되어 있음 

    public int Get()            //내부 변수 값을 외부로 가져오는 함수 
    {
        return data;         
    }

    public void Set(int _data)  //외부의 매개변수 값을 변수 값으로 설정
    {
        data = _data;
    }

}
