using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MonsterManager", menuName = "ScriptableObject/MonsterManager")]
public class MonsterManager
{
    [SerializeField]
    public GameObject[] monsters;

    GameObject spawnerPrefab;
    GameObject spawner;

    public void Init()
    {
        spawnerPrefab = Resources.Load<GameObject>("Spawner");

        LoadAllMonsterStats();
        SetSpawner();
    }

    void LoadAllMonsterStats()
    {
        monsters = Resources.LoadAll<GameObject>("Monsters");
        for (int count = 0; count < monsters.Length; count++)
        {
            monsters[count].GetComponent<MonsterController>().stat = Managers.Data.MonsterList.Find(name => name.objectName == monsters[count].name);
            Debug.Log(monsters[count].name + monsters[count].GetComponent<MonsterController>().stat.attack);
        }
    }

    void SetSpawner()
    {
        spawner = GameObject.Instantiate<GameObject>(spawnerPrefab);
    }
}
