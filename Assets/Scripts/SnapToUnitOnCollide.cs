using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Snap object's position to whole numbers
public class SnapToUnitOnCollide : MonoBehaviour
{
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D _collision)
    {
        _rb.position = Vector2Int.RoundToInt(_rb.position);
    }
}
