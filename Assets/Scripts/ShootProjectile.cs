using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile; // projectile to shoot
    public Vector2 shootDirection; // direction to shoot the projectile at
    public float projectileSpeed;
    public float shootCooldown;
    private float timer;
    private bool isFrozen; // if the object is frozen or not
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        isFrozen = enemy.isFrozen;
        enemy.OnFrozenStateChanged += UpdateFrozenState;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            timer += Time.deltaTime;

            // check if cooldown is over
            if (timer >= shootCooldown)
            {
                Shoot();
                timer = 0f;
            }
        }
    }

    void UpdateFrozenState(bool frozen)
    {
        isFrozen = frozen;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
    }
}
