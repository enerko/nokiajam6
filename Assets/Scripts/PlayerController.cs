using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] GameObject sword;
    [SerializeField] int attackRange = 4;
    [SerializeField] float attackTime = 0.25f;

    private Rigidbody2D _rb;

    // private bool isTimeFrozen = true;

    private float _horizontal;
    private float _vertical;
    private bool _attacking = false;
    private Vector3 _targetPos;
    private Vector3 _origPos;

    private int _direction;
    private int _health;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _direction = 1;
    }

    // get input
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _direction = _horizontal < 0 ? -1 : 1;

        // sword code
        if (Input.GetKeyDown(KeyCode.Space) && !_attacking)
        {
            _attacking = true;
            _origPos = sword.transform.position;
            _targetPos = _origPos + _direction * attackRange * Vector3.right;
            StartCoroutine(MoveSword());
        }
    }

    // movement
    private void FixedUpdate()
    {
        if (_attacking)
        {  // if attacking, stop moving
            _rb.velocity = Vector2.zero;
            return;
        }  
        
        // else
        if (_horizontal != 0)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_horizontal) * runSpeed, 0);
        } else if (_vertical != 0)
        {
            _rb.velocity = new Vector2(0, Mathf.Sign(_vertical) * runSpeed);
        } else 
        {
            _rb.velocity = Vector2.zero;
        }
    }

    IEnumerator MoveSword()
    {
        // Move the sword to target position, then return it to original position
        float elapsedTime = 0f;

        while (elapsedTime < attackTime)
        {
            sword.transform.position = Vector3.Lerp(_origPos, _targetPos, elapsedTime / attackTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sword.transform.position = _targetPos;
        yield return new WaitForSeconds(0.5f);

        elapsedTime = 0f;
        while (elapsedTime < attackTime)
        {
            sword.transform.position = Vector3.Lerp(_targetPos, _origPos, elapsedTime / attackTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sword.transform.position = _origPos;
        _attacking = false;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}