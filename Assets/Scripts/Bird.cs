using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 3;
    [SerializeField] Animator _animator;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    bool _isLaunched;
    bool _hasCrashed;

    public bool HasCrashed { get { return _hasCrashed; } set { _hasCrashed = value; _animator.SetBool("HasCrashed", value); } }
    public bool IsLaunched { get { return _isLaunched; } set { _isLaunched = value; } }
    public bool IsDragging { get; set; }
    public Vector2 StartPosition { get { return _startPosition; } set { _startPosition = value; } }

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void OnMouseDown()
    {
        if (IsLaunched == false)
        {
            _spriteRenderer.color = Color.red;
        }
    }

    void OnMouseUp()
    {
        if (IsLaunched == false)
        {
            Vector2 currentPosition = _rigidbody2D.position;
            Vector2 direction = StartPosition - currentPosition;
            //direction.Normalize();

            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(direction * _launchForce);
            IsLaunched = true;

            _spriteRenderer.color = Color.white;
            IsDragging = false;
        }
    }

    void OnMouseDrag()
    {
        if (IsLaunched == false)
        {
            IsDragging = true;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 desiredPosition = mousePosition;
            if (desiredPosition.x > StartPosition.x)
            {
                desiredPosition.x = StartPosition.x;
            }

            float distance = Vector2.Distance(desiredPosition, StartPosition);
            if (distance > _maxDragDistance)
            {
                Vector2 direction = desiredPosition - StartPosition;
                direction.Normalize();
                desiredPosition = StartPosition + (direction * _maxDragDistance);
            }

            _rigidbody2D.position = desiredPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasCrashed == false)
        {
            HasCrashed = true;
            StartCoroutine(ResetAfterDelay());
        }
    }

    
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = StartPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
        transform.rotation = Quaternion.identity;
        HasCrashed = false;
        IsLaunched = false;
    }




    
}
