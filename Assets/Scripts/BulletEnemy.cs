using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speedBulletEnemy;
    Rigidbody2D rb;
    private SpriteRenderer sprite1;
    float dirBullet;
    void Start()
    {
        sprite1 = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (sprite1.flipX)
        {
            dirBullet = 1;
        }
        else
        {
            dirBullet = -1;
        }
        rb.velocity=dirBullet*transform.right*speedBulletEnemy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject,5);
    }
}
