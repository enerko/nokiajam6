using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemies can have different attacks, ranged/melee

    public void Death()
    {
        Globals.OnCharacterDeath("Enemy");
        // play death anim
        Destroy(gameObject);
    }

}
