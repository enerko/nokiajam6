using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hi");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Death();

        }

        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
