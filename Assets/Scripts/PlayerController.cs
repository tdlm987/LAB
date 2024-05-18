using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private enum State { idle,run,jump,crouch,shoot}
    private State state=State.idle;
    private Animator anim;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        sprite=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity=new Vector2 (dirX*moveSpeed, rb.velocity.y);
        if (dirX > 0)
        {
            sprite.flipX = false;
            state = State.run;
        }
        else if (dirX < 0)
        {
            sprite.flipX = true;
            state = State.run;
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            state = State.jump;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            state = State.crouch;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType= RigidbodyType2D.Dynamic;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            state = State.shoot;
            rb.bodyType = RigidbodyType2D.Static;
            rb.velocity=new Vector2 (rb.velocity.x,rb.velocity.y);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        anim.SetInteger("state", (int)state);
        StateSwitch();
    }
    private void StateSwitch()
    {
        if (Mathf.Abs(rb.velocity.y) > .1f)
        {
            state=State.jump;
        }
        else if (state==State.jump)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x)> 2f)
        {
            state = State.run;
        }
        else
        {
            state=State.idle;
        }
    }
}
