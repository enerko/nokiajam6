using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemies can have different attacks, ranged/melee, but all must be able to freeze
    public bool isFrozen; 
    public delegate void FrozenStateChanged(bool frozen);
    public event FrozenStateChanged OnFrozenStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            OnFrozenStateChanged?.Invoke(isFrozen);
        }
    }

    public void Death()
    {
        // play death anim
        Destroy(gameObject);
    }

}
