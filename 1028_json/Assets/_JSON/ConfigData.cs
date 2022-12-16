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
                _instance = (ConfigData)FindObjectOfType(typeof(ConfigData));  //���º�ȯ (ConfigData) + configdata��� ������ ã��

                if (_instance == null)  //�׷��� ���ٸ�
                {
                    _instance = new GameObject("ConfigData", typeof(ConfigData)).GetComponent<ConfigData>();    //�̱��� -> 
                }
            }
            return _instance;
        }

    }

    void read_config() 
    {
        string text = Utils.ReadTextFromFile("config/config.json");     //Utils script���Ƿ� �̵� -> ReadTextFromFile() �ҷ�����
        if (text == null) 
        {
            Debug.Log("can not find config.json file!");
            return;
        }

        var node = JSON.Parse(text);    //����
        string g_name  = node["name"].Value;
        string g_id    = node["id"].Value;
        int    g_score = int.Parse(node["score"].Value); 
        float  g_rate  = float.Parse(node["rate"].Value); 
    }


    void Awake()    //start()���� ���� �����϶�.
    {
        ///DontDestroyOnLoad(this);  
        DontDestroyOnLoad(gameObject);  //�빮�� -> ��� ����obj, �ҹ��� -> Ŭ���� �� �ڽ� 

        read_config(); 
    }




}


