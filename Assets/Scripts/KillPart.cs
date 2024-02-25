using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Part that on touch, kills player
public class KillPart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Death();
            Globals.RestartCurrentLevel(1);
        }
    }
}
