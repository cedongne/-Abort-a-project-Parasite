using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        GameObject monster = Instantiate(Resources.Load<GameObject>("Monsters/전등"), transform);
        monster.GetComponent<MonsterController>().stat = Managers.Data.MonsterList.Find(name => name.objectName == "전등");
    }
}
