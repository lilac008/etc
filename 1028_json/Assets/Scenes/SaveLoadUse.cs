using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadUse : MonoBehaviour
{
    CharacterData m_CharData;

    void Start()
    {
        m_CharData = SaveLoad.LoadData();

        if (m_CharData = null)
        {
            m_CharData = new CharacterData();
            m_CharData.char_name = "monkey";
            m_CharData.hp = 100;
            m_CharData.score = 0;
            SaveLoad.SaveData(m_CharData);
            Debug.Log("Make new game data!");
        }
        else 
        {
            Debug.Log(m_CharData.char_name);
            Debug.Log(m_CharData.hp);
            Debug.Log(m_CharData.score);
        }

    }

    void Update()
    {
        
    }
}
