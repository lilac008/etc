using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;


public class VoiceManager : MonoBehaviour
{
    private GCSpeechRecognition gcSpeech;

    private static VoiceManager instance = null;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        //

    }

    private static VoiceManager instance 
    {
        get 
        {
            if (instance == null) 
            { 
            }

        }    


    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
