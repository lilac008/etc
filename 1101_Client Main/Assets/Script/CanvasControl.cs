using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public ClientMain m_client;
    public InputField m_inputfield;

    public void on_submit_input()
    {
        m_client.SendData(m_inputfield.text);
        m_inputfield.text = "";
        m_inputfield.ActivateInputField();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
