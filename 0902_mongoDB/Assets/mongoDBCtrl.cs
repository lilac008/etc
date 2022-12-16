using MongoDB.Bson;           //json���·� ���⿡ bson���� �̸��� ��������
using MongoDB.Driver;         //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mongoDBCtrl : MonoBehaviour
{

    const string MONGO_URI = "mongodb+srv://test11:test11@cluster0.ewqmtjs.mongodb.net/?retryWrites=true&w=majority"; //����DB�� ���� �ּ� ���� �� ��й�ȣ���� �Է�
    const string DATABASE_NAME = "test11";
    MongoClient client;
    IMongoDatabase db;
    IMongoCollection<GameData> db_col;

    void Start()
    {
        ////////�ʱ�ȭ�۾�///////////
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

    void db_login()      ///�α��� 
    {
       client = new MongoClient(MONGO_URI);
    }

    void get_database()  ///�����ͺ��̽� ��������
    {
        db = client.GetDatabase("DATABASE_NAME");
        Debug.Log(db);
    }

    void get_collection() ///�����ͺ��̽����� �÷��� ��������
    {
        db_col = db.GetCollection<GameData>("test11_col");     ///GameData���·� collection - test11_col ��������
        ///Debug.Log(db_col);
    }

    void db_insert(string name, int score)  ///�����ͺ��̽��� ����������ֱ�
    {
        /////////////////////�߰�
        if (db_exist(name)) 
        {
            Debug.Log("Name is exist" + name);
            return;
        }

        ////////////////////����
        GameData _g = new GameData();
        ///Debug.Log(score);
        ///Debug.Log(_g.highscore);
        _g.name = name;
        _g.highscore = score;

        db_col.InsertOne(_g);     ///InsertOne : �ѹ��� �����͸� ����־��/�����϶�

    }

    void db_all_view()  /// �������� ��� db�����͸� ��������
    {
        List<GameData> user_list = db_col.Find(user => true).ToList();  ///db_col�� user�� ã�Ƽ� ����Ʈȭ ��Ų��.

        GameData[] user_data = user_list.ToArray();                      ///����Ʈ�� �迭�� ����
        for (int i = 0; i < user_data.Length; i++) 
        {
            /// Debug.Log(user_data[i].name + ":" + user_data[i].highscore); ///����Ƽ�� ������� �ȵǼ� Debug.Log�� �޼����� ��� ����Ѵ�.
        }
    }

    void db_find(string name) 
    {
        BsonDocument _bson = new BsonDocument { { "name", name } }; ///json������ ��ť���͸�, "�ʵ��̸�", �Ű�����
        
        List<GameData> user_list = db_col.Find(_bson).ToList();     ///����Ʈȭ
        GameData[] user_data = user_list.ToArray();                 ///����Ʈ�� �ӵ��� �������Ƿ� �迭ȭ
        for (int i =0; i<user_data.Length; i++) 
        {
            Debug.Log(user_data[i].name + ":" + user_data[i].highscore);
        }
    }

    bool db_exist(string name) 
    {
        BsonDocument _bson = new BsonDocument { {"name", name } };  //json������ ��ť���͸�,

        List<GameData> user_list = db_col.Find(_bson).ToList();
        GameData[] user_data = user_list.ToArray() ;
        if (user_data.Length == 0) 
            return false;
            return true;
    }

    void db_remove(string name)    ///������ ����
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };
        db_col.DeleteOne(_bson);
    }

    void db_removes(string name)   ///
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };
        db_col.DeleteMany(_bson);

    }

    void db_remove_all()         ///��� ����
    {
        BsonDocument _bson = new BsonDocument { };  ///��� �����ؼ� 
        db_col.DeleteMany(_bson);
    }


    void db_update(string name, int score) 
    {
        BsonDocument _bson_search = new BsonDocument { { "name", name } };
        BsonDocument _bson_update = new BsonDocument { { "name", name },{ "higcore", score} }; //

        db_col.FindOneAndUpdate(_bson_search, _bson_update);
    }






}
