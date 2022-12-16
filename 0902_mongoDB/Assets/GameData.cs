using MongoDB.Bson;           //json���·� ���⿡ bson���� �̸��� ��������
using MongoDB.Driver;         //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData //��ü�� ����� �ʿ䰡 ���� ������ MonoBehaviour�� �����.
{
    public ObjectId _id { get; set; }   //ObjectId(���Ƿ��̵�) : ���̵� ���� �ʵ�, get; set;���� ���������� ���� ������ �� �ִ�.
    
    public string name { get; set; }
    
    public int highscore { get; set; } 




}
