using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Part that on touch, kills player
public class DestroyOnPlayerOrTagHit : MonoBehaviour
{
    [SerializeField] string targetTag = "DestroyBullets";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == targetTag)
        {
            Destroy(gameObject);
        }
    }
}
