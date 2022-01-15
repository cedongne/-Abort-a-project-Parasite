using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGoldItem : MonoBehaviour, IDropItem
{
    public void Gain()
    {
        PlayerController.playerStat.gold += Random.Range(10, 20);
        Debug.Log("+" + PlayerController.playerStat.gold + "GOLD");
        gameObject.SetActive(false);
    }
}
