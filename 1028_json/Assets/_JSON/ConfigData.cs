using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using System.Text;


public class ConfigData : MonoBehaviour
{
    static ConfigData _instance = null;

    public string g_name;
    public string g_id;
    public int    g_score;
    public float  g_rate;


    public static ConfigData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (ConfigData)FindObjectOfType(typeof(ConfigData));  //형태변환 (ConfigData) + configdata라는 파일을 찾아

                if (_instance == null)  //그래도 없다면
                {
                    _instance = new GameObject("ConfigData", typeof(ConfigData)).GetComponent<ConfigData>();    //싱글톤 -> 
                }
            }
            return _instance;
        }

    }

    void read_config() 
    {
        string text = Utils.ReadTextFromFile("config/config.json");     //Utils script정의로 이동 -> ReadTextFromFile() 불러오기
        if (text == null) 
        {
            Debug.Log("can not find config.json file!");
            return;
        }

        var node = JSON.Parse(text);    //분해
        string g_name  = node["name"].Value;
        string g_id    = node["id"].Value;
        int    g_score = int.Parse(node["score"].Value); 
        float  g_rate  = float.Parse(node["rate"].Value); 
    }


    void Awake()    //start()보다 먼저 실행하라.
    {
        ///DontDestroyOnLoad(this);  
        DontDestroyOnLoad(gameObject);  //대문자 -> 모든 게임obj, 소문자 -> 클래스 나 자신 

        read_config(); 
    }




}


