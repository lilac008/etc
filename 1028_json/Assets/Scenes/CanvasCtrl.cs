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

    //��ư�� Ŭ������ �� CanvasCtrl ��ü�� �ִ� script�� ��� -> ���� �Լ��� ����. scene1�� �ı��ϰ� scene2�� ��. 
}
