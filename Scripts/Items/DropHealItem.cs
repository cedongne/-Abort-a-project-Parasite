using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHealItem : MonoBehaviour, IDropItem
{
    PlayerController Player;
    public void Gain()
    {
        PlayerController.playerStat.hp -= 10;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.GetComponent<PlayerController>();
        }
    }
}
