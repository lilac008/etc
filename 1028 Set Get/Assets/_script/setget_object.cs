using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setget_object : MonoBehaviour
{
    void Start()
    {
       SetGet SG = GameObject.Find("setget").GetComponent<SetGet>(); ///setget class를 찾아라

       SG.data = 100;

       Debug.Log(SG.data);

    }

    void Update()
    {
        
    }
}
