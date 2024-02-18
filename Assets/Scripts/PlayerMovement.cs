using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    private bool isTimeFrozen = true;

    private float horizontal;
    private float vertical;

    public float runSpeed = 10.0f;
    public GameObject sword;
    public int attackRange = 4;
    public float attackTime = 0.25f;

    private bool isAttacking = false;
    private Vector3 targetPos;
    private Vector3 origPos;

    private int direction;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = horizontal < 0 ? -1 : 1;

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isAttacking = true;
            origPos = sword.transform.position;
            targetPos = origPos + direction * attackRange * Vector3.right;
            StartCoroutine(MoveSword());
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking) // Move only if player is not attacking
        {
            rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator MoveSword()
    {
        // Move the sword to target position, then return it to original position
        float elapsedTime = 0f;

        while (elapsedTime < attackTime)
        {
            sword.transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / attackTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sword.transform.position = targetPos;
        yield return new WaitForSeconds(0.5f);

        elapsedTime = 0f;
        while (elapsedTime < attackTime)
        {
            sword.transform.position = Vector3.Lerp(targetPos, origPos, elapsedTime / attackTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sword.transform.position = origPos;
        isAttacking = false;
    }

}