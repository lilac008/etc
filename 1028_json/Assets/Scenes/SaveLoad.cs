using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;   //
using System.IO;                                        //

public class SaveLoad : MonoBehaviour
{
    public static void SaveData( CharacterData _char) 
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/game.dat";     // Application.persistentDataPath : [안드로이드 External] 파일 읽기 쓰기 가능     
        Debug.Log(path);                                                // http://memocube.blogspot.com/2014/04/blog-post.html

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, _char);
        stream.Close();
    }

    public static CharacterData LoadData()      //void 반환하지 않는게 아닌 CharacterData로 반환
    {
        string path = Application.persistentDataPath + "/game.dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CharacterData data = formatter.Deserialize(stream) as CharacterData;        /// 형 변환
            /// CharacterData data = (CharacterData) formatter.Deserialize(stream);
           
            stream.Close();

            return data;
        }
        else 
        {
            return null;
        }

    }
}
