using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public List<PlayerStat> ItemList;
    public List<MonsterStat> MonsterList;

    public void Init()
    {
        ItemList = new List<PlayerStat>();
        MonsterList = new List<MonsterStat>();
        LoadBase();
    }

    void ParsingItemJson(string file, List<PlayerStat> _itemList)
    {
        JsonData jsonFile = JsonMapper.ToObject(file);

        for (int count = 0; count < jsonFile.Count; count++)
        {
            string name = jsonFile[count][0].ToString();
            string type = jsonFile[count][1].ToString();
            string tmpGrade = jsonFile[count][2].ToString();
            string tmpAttack = jsonFile[count][3].ToString();
            string tmpMaxHp = jsonFile[count][4].ToString();
            string tmpAttackSpeed = jsonFile[count][5].ToString();
            string tmpMoveSpeed = jsonFile[count][6].ToString();
            string tmpCriticalChance = jsonFile[count][7].ToString();
            string tmpCoolDown = jsonFile[count][8].ToString();

            int grade = int.Parse(tmpGrade);
            int attack = int.Parse(tmpAttack);
            int maxHp = int.Parse(tmpMaxHp);
            float attackSpeed = float.Parse(tmpAttackSpeed);
            float moveSpeed = float.Parse(tmpMoveSpeed);
            int criticalChance = int.Parse(tmpCriticalChance);
            int coolDown = int.Parse(tmpCoolDown);

            PlayerStat tempItem = new PlayerStat(name, type, grade, attack, maxHp, attackSpeed, moveSpeed, criticalChance, coolDown);
            _itemList.Add(tempItem);
        }
    }
    void ParsingMonsterJson(string file, List<MonsterStat> _monsterList)
    {
        JsonData jsonFile = JsonMapper.ToObject(file);

        for (int count = 0; count < jsonFile.Count; count++)
        {
            string name = jsonFile[count][0].ToString();
            string type = jsonFile[count][1].ToString();
            string tmpGrade = jsonFile[count][2].ToString();
            string tmpAttack = jsonFile[count][3].ToString();
            string tmpMaxHp = jsonFile[count][4].ToString();
            string tmpAttackSpeed = jsonFile[count][5].ToString();
            string tmpMoveSpeed = jsonFile[count][6].ToString();

            int grade = int.Parse(tmpGrade);
            int attack = int.Parse(tmpAttack);
            int maxHp = int.Parse(tmpMaxHp);
            float attackSpeed = float.Parse(tmpAttackSpeed);
            float moveSpeed = float.Parse(tmpMoveSpeed);

            MonsterStat tempItem = new MonsterStat(name, type, grade, attack, maxHp, attackSpeed, moveSpeed);
            _monsterList.Add(tempItem);
        }
    }

    void LoadBase()
    {
        string itemJsonPath = File.ReadAllText(Application.dataPath + "/Datas/Databases/JSON/ItemDB.json");
        string monsterJsonPath = File.ReadAllText(Application.dataPath + "/Datas/Databases/JSON/MonsterDB.json");
        ParsingItemJson(itemJsonPath, ItemList);
        ParsingMonsterJson(monsterJsonPath, MonsterList);
    }
}
