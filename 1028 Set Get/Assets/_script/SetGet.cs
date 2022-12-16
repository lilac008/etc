using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGet : MonoBehaviour
{
    private int _data;          /// 숨겨놓았음을 표시하기 위해 _로 표시

    public int data 
    {
        get                         //내부 변수 값을 외부로 가져오는 함수 
        {                           
            Debug.Log("get 호출");
            return _data; 
        }

        set                         //외부의 매개변수 값을 변수 값으로 설정    
        { 
            Debug.Log("set 호출");
            _data = value;          //value;는 data에 어떤 걸 집어넣을 때 
        }
    }

}
