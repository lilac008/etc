using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    Renderer m_rend;


    void Start()
    {
        m_rend = GetComponent<Renderer>();
    }

    bool is_color_tr = false;
    float one_color = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            m_rend.material.color = new Color(0, 0, 1);   /// B �Ķ�
            /*
            m_rend.material.color = new Color(0,0,0);   /// ������
            m_rend.material.color = new Color(1,1,1);   /// ���
            m_rend.material.color = new Color(1,0,0);   /// R ����
            m_rend.material.color = new Color(0,1,0);   /// G �ʷ�
            m_rend.material.color = new Color(0,0,1);   /// B �Ķ�
            */
        }
        if (is_color_tr)        ///�ð��� ���� �� ��ȭ 
        {
            one_color += Time.deltaTime * 0.25f;        // 0.5f = 1/2,    0.25f = 1/4
            if (one_color > 1) 
                one_color = 1;
            m_rend.material.color = new Color(one_color, one_color, 1);
        }


        
    }
}
