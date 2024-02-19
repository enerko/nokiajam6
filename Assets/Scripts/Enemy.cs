using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        health -= 10;
        Death();
    }

    private void Death()
    {
        // play death anim
        Destroy(gameObject);
    }
}
