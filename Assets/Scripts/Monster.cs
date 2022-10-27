using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] bool _hasDied;
    [SerializeField] ParticleSystem _particleSystem;

    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    PolygonCollider2D _polygonCollider2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            HasDied = true;
        }
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            return true;
        }
        
        if (collision.contacts[0].normal.y < -0.5)
        {
            return true;
        }

        if ( Math.Abs(collision.contacts[0].relativeVelocity.y) > 5 || Math.Abs(collision.contacts[0].relativeVelocity.x) > 5)
        {
            return true;
        }

        return false;
    }

    public bool HasDied { get { return _hasDied; } set { _hasDied = value; Die(); } }

    void Die()
    {
        _spriteRenderer.enabled = false;
        _polygonCollider2D.enabled = false;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
        _particleSystem.Play();
        StartCoroutine(Disable());

    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
