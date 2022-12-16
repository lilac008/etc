using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseJSON : MonoBehaviour
{
    void Start()
    {
        Debug.Log(ConfigData.Instance.g_name);
        Debug.Log(ConfigData.Instance.g_id);
        Debug.Log(ConfigData.Instance.g_score);
        Debug.Log(ConfigData.Instance.g_rate);

    }

    void Update()
    {
        
    }
}
