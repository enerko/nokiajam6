using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to a killpart to make it disappear and reappear
public class Flicker : MonoBehaviour
{
    [SerializeField] bool startOn = true;
    [SerializeField] float interval;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    private float _timer = 0;
    private bool _frozen = false;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _renderer.enabled = startOn;
        _collider.enabled = startOn;

        FreezeButton.OnFrozenStateChanged += OnFreeze;
    }

    // Update is called once per frame
    void Update()
    {
        if (_frozen) return;

        _timer += Time.deltaTime;

        if (_timer >= interval) {
            _timer = 0;
            _renderer.enabled = !_renderer.enabled;
            _collider.enabled = !_collider.enabled;
        }
    }

    // disable laser on freeze
    void OnFreeze(bool val) {
        _frozen = val;
        _renderer.enabled = !val;
        _collider.enabled = !val;
        _timer = 0;
    }
}
