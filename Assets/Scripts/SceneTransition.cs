using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] AudioClip winSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Globals.PlayClip(winSound);
            Destroy(collision.gameObject);
            Globals.LoadNextLevel(2);
        }
    }
}
