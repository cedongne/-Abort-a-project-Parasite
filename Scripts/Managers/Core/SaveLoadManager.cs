using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager
{
    SaveData saveData;

    string SAVE_DIRECTORY_PATH;
    string SAVE_FILE_NAME;

    GameObject player;

    public void Init()
    {
        saveData = new SaveData();

        player = GameObject.Find("Player");

        SAVE_DIRECTORY_PATH = Application.dataPath + "/Datas";
        SAVE_FILE_NAME = "/Save.json";
    }

    public void Save()
    {
        saveData.playerTransform = player.transform;
        saveData.playerCharacter = PlayerController.playerStat;

        string saveJson = JsonUtility.ToJson(saveData);

        if (!Directory.Exists(SAVE_DIRECTORY_PATH))
            Directory.CreateDirectory(SAVE_DIRECTORY_PATH);

        File.WriteAllText(SAVE_DIRECTORY_PATH + SAVE_FILE_NAME, saveJson);
    }

    public void Load()
    {
        string saveJson = File.ReadAllText(SAVE_DIRECTORY_PATH + SAVE_FILE_NAME);

        saveData = JsonUtility.FromJson<SaveData>(saveJson);
        player.transform.position = saveData.playerTransform.position;
        player.transform.rotation = saveData.playerTransform.rotation;
        PlayerController.playerStat = saveData.playerCharacter;

        File.Delete(SAVE_DIRECTORY_PATH + SAVE_FILE_NAME);
    }

    public void NewGame()
    {
        File.Delete(SAVE_DIRECTORY_PATH + SAVE_FILE_NAME);
    }

}


