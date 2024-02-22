using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // this should be attached to objects with enemy component

    public GameObject projectile; // projectile to shoot
    public float projectileSpeed;
    public float shootCooldown;

    private float _timer;
    public bool _isFrozen; // if the object is frozen or not

    // Start is called before the first frame update
    void Start()
    {
        _isFrozen = false;
        FreezeButton.OnFrozenStateChanged += UpdateFrozenState;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isFrozen)
        {
            _timer += Time.deltaTime;

            // check if cooldown is over
            if (_timer >= shootCooldown)
            {
                Shoot();
                _timer = 0f;
            }
        }
    }

    void UpdateFrozenState(bool frozen)
    {
        _isFrozen = frozen;
    }

    void Shoot()
    {
        Vector3 localOffset = new Vector3(2, 2, 0);
        Vector3 worldOffset = transform.TransformDirection(localOffset);
        Vector3 bulletStartPos = Vector3Int.RoundToInt(transform.position + worldOffset);
        GameObject bullet = Instantiate(projectile, bulletStartPos, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
    }
}
