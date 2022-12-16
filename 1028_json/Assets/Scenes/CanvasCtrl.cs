using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCtrl : MonoBehaviour
{
    public void goto_scene2()
    {
        SceneManager.LoadScene("save1");
    }

    public void goto_scene1()
    {
        SceneManager.LoadScene("save1");
    }

    //버튼을 클릭했을 때 CanvasCtrl 객체에 있는 script를 사용 -> 관련 함수를 실행. scene1을 파괴하고 scene2로 감. 
}
