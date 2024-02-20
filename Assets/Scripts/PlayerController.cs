using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] GameObject sword;

    private Rigidbody2D _rb;

    private float _horizontal;
    private float _vertical;
    private bool _attacking = false;
    private Vector3 _targetPos;
    private Vector3 _origPos;

    private List<KeyCode> _inputList;  // always push to this in chronological order
    private KeyCode[] _arrowKeys = { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow };

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputList = new();
    }

    // get input
    void Update()
    {
        // Update move direction if input key is pressed down
        foreach (KeyCode k in _arrowKeys)
        {
            if (Input.GetKeyDown(k))
            {
                _inputList.Add(k);
            }
        }

        // When key input ends, remove it from the list
        foreach (KeyCode k in _arrowKeys)
        {
            if (Input.GetKeyUp(k))
            {
                _inputList.Remove(k);
            }
        }

        // sword code
        if (Input.GetKeyDown(KeyCode.Space) && !_attacking)
        {
            StartCoroutine(Attack());
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
        Vector2 direction;
        switch (_inputList.DefaultIfEmpty(KeyCode.None).Last())
        {
            case KeyCode.UpArrow:  // up
                direction = new Vector2(0, 1);
                break;
            case KeyCode.RightArrow:  // right
                direction = new Vector2(1, 0);
                break;
            case KeyCode.DownArrow:  // down
                direction = new Vector2(0, -1);
                break;
            case KeyCode.LeftArrow:  // left
                direction = new Vector2(-1, 0);
                break;
            default:
                direction = new Vector2(0, 0);
                break;
        }

        _rb.velocity = direction * runSpeed;
    }

    IEnumerator Attack()
    {
        _attacking = true;
        _origPos = Vector3Int.RoundToInt(sword.transform.localPosition);

        // Move the sword to target position, then return it to original
        Vector3 targetPos;
        Quaternion targetRot;

        // just hard code target pos because yeah
        switch (_inputList.DefaultIfEmpty(KeyCode.None).Last())
        {
            case KeyCode.UpArrow:  // up
                targetPos = new Vector3(3, 4, 1);
                targetRot = Quaternion.AngleAxis(90, Vector3.forward);
                break;
            case KeyCode.RightArrow:  // right
                targetPos = new Vector3(4, 2, 1);
                targetRot = Quaternion.AngleAxis(0, Vector3.forward);
                break;
            case KeyCode.DownArrow:  // down
                targetPos = new Vector3(3, -2, 1);
                targetRot = Quaternion.AngleAxis(90, Vector3.forward);
                break;
            default:  // default to left
                targetPos = new Vector3(-2, 2, 1);
                targetRot = Quaternion.AngleAxis(0, Vector3.forward);
                break;
        }

        sword.transform.localPosition = targetPos;
        sword.transform.rotation = targetRot;

        yield return new WaitForSeconds(0.5f);

        sword.transform.localPosition = _origPos;
        sword.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        _attacking = false;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}