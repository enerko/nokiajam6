using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeButton : MonoBehaviour
{
    private bool _isFrozen;
    private float _timer;

    public float freezeTime;
    public delegate void FrozenStateChanged(bool frozen);
    public static event FrozenStateChanged OnFrozenStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        _isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFrozen)
        {
            _timer += Time.deltaTime;
            // if freezeTime is over, then reset isFrozen and timer
            if (_timer >= freezeTime)
            {
                _isFrozen = false;
                OnFrozenStateChanged?.Invoke(_isFrozen);
                _timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player presses the button then freeze enemies
        if (collision.gameObject.tag == "Player")
        {
            _isFrozen = true;
            OnFrozenStateChanged?.Invoke(_isFrozen);
        }
    }
}
