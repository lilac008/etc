using MongoDB.Bson;           //json형태로 쓰기에 bson으로 이름을 지은듯함
using MongoDB.Driver;         //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mongoDBCtrl : MonoBehaviour
{

    const string MONGO_URI = "mongodb+srv://test11:test11@cluster0.ewqmtjs.mongodb.net/?retryWrites=true&w=majority"; //몽고DB로 가는 주소 복붙 후 비밀번호란에 입력
    const string DATABASE_NAME = "test11";
    MongoClient client;
    IMongoDatabase db;
    IMongoCollection<GameData> db_col;

    void Start()
    {
        ////////초기화작업///////////
        db_login();
        get_database();
        get_collection();
        ////////////////////////////

        //db_insert("min1", 0);
        //db_all_view();
        //db_find("min2");
        //db_remove("min1");
        //db_removes("min1");
    }

    void db_login()      ///로그인 
    {
       client = new MongoClient(MONGO_URI);
    }

    void get_database()  ///데이터베이스 가져오기
    {
        db = client.GetDatabase("DATABASE_NAME");
        Debug.Log(db);
    }

    void get_collection() ///데이터베이스안의 컬렉션 가져오기
    {
        db_col = db.GetCollection<GameData>("test11_col");     ///GameData형태로 collection - test11_col 가져오기
        ///Debug.Log(db_col);
    }

    void db_insert(string name, int score)  ///데이터베이스에 데이터집어넣기
    {
        /////////////////////추가
        if (db_exist(name)) 
        {
            Debug.Log("Name is exist" + name);
            return;
        }

        ////////////////////기존
        GameData _g = new GameData();
        ///Debug.Log(score);
        ///Debug.Log(_g.highscore);
        _g.name = name;
        _g.highscore = score;

        db_col.InsertOne(_g);     ///InsertOne : 한번만 데이터를 집어넣어라/실행하라

    }

    void db_all_view()  /// 서버에서 모든 db데이터를 가져오기
    {
        List<GameData> user_list = db_col.Find(user => true).ToList();  ///db_col에 user를 찾아서 리스트화 시킨다.

        GameData[] user_data = user_list.ToArray();                      ///리스트를 배열로 변경
        for (int i = 0; i < user_data.Length; i++) 
        {
            /// Debug.Log(user_data[i].name + ":" + user_data[i].highscore); ///유니티는 디버깅이 안되서 Debug.Log로 메세지를 띄워 사용한다.
        }
    }

    void db_find(string name) 
    {
        BsonDocument _bson = new BsonDocument { { "name", name } }; ///json형태의 다큐멘터리, "필드이름", 매개변수
        
        List<GameData> user_list = db_col.Find(_bson).ToList();     ///리스트화
        GameData[] user_data = user_list.ToArray();                 ///리스트는 속도가 떨어지므로 배열화
        for (int i =0; i<user_data.Length; i++) 
        {
            Debug.Log(user_data[i].name + ":" + user_data[i].highscore);
        }
    }

    bool db_exist(string name) 
    {
        BsonDocument _bson = new BsonDocument { {"name", name } };  //json형태의 다큐멘터리,

        List<GameData> user_list = db_col.Find(_bson).ToList();
        GameData[] user_data = user_list.ToArray() ;
        if (user_data.Length == 0) 
            return false;
            return true;
    }

    void db_remove(string name)    ///데이터 삭제
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };
        db_col.DeleteOne(_bson);
    }

    void db_removes(string name)   ///
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };
        db_col.DeleteMany(_bson);

    }

    void db_remove_all()         ///모두 삭제
    {
        BsonDocument _bson = new BsonDocument { };  ///모두 선택해서 
        db_col.DeleteMany(_bson);
    }


    void db_update(string name, int score) 
    {
        BsonDocument _bson_search = new BsonDocument { { "name", name } };
        BsonDocument _bson_update = new BsonDocument { { "name", name },{ "higcore", score} }; //

        db_col.FindOneAndUpdate(_bson_search, _bson_update);
    }






}
