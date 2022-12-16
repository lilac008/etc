using MongoDB.Bson;           //json형태로 쓰기에 bson으로 이름을 지은듯함
using MongoDB.Driver;         //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData //객체로 사용할 필요가 없기 때문에 MonoBehaviour은 지운다.
{
    public ObjectId _id { get; set; }   //ObjectId(정의로이동) : 아이디 생성 필드, get; set;으로 내부적으로 값을 조절할 수 있다.
    
    public string name { get; set; }
    
    public int highscore { get; set; } 




}
