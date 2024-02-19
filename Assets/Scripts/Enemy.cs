using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemies can have different attacks, ranged/melee

    public void Death()
    {
        // play death anim
        Destroy(gameObject);
    }

}
