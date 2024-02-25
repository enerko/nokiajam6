using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeButton : MonoBehaviour
{
    private bool _isFrozen;
    private float _timer;
    private SpriteRenderer _spriteRenderer;

    public float freezeTime;
    public delegate void FrozenStateChanged(bool frozen);
    public static event FrozenStateChanged OnFrozenStateChanged;
    public AudioClip freezeSound;
    public Sprite buttonPressed;
    public Sprite buttonReleased;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        _isFrozen = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = buttonReleased;
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
                _spriteRenderer.sprite = buttonReleased;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player presses the button then freeze enemies
        if (collision.gameObject.tag == "Player" && !_isFrozen)
        {
            _isFrozen = true;
            OnFrozenStateChanged?.Invoke(_isFrozen);
            Globals.PlayClip(freezeSound);
            _spriteRenderer.sprite = buttonPressed;
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
        }
    }
}
