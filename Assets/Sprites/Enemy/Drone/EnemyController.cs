using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform posA, posB;
    Vector2 posTarget;
    [SerializeField] private float speedDrone;
    private SpriteRenderer spriteDrone;
    private Animator animDrone;
    [SerializeField] private Transform posPlayer;
    float speedDroneShoot;
    [SerializeField] private GameObject bulletDrone;
    [SerializeField] private Transform bulletDronePoint;
    float dirXBullet;
    [SerializeField] private AudioSource soundDeath;
    [SerializeField] private AudioSource soundShootByDrone;
    private Rigidbody2D rb;
    private Collider2D coll;
    void Start()
    {
        posTarget = posB.position;
        animDrone = GetComponent<Animator>();
        spriteDrone = GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        coll= GetComponent<Collider2D>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            posTarget = posB.position;
            spriteDrone.flipX = true;
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            posTarget = posA.position;
            spriteDrone.flipX=false;
        }
        transform.position=Vector2.MoveTowards(transform.position,posTarget,speedDrone*Time.deltaTime);

        //Nếu người chơi gần con tàu, nó sẽ dừng lại hướng vào người chơi và bắn
        if (Mathf.Abs(posPlayer.position.x - transform.position.x) < 7)
        {
            if (transform.position.x > posPlayer.position.x)
            {
                spriteDrone.flipX = false;
            }
            else
            {
                spriteDrone.flipX = true;
            }
            speedDrone = 0;
            Shoot();
        }
        else
        {
            speedDrone=5;
        }
    }
    void Shoot()
    {
        speedDroneShoot+=Time.deltaTime;
        if (speedDroneShoot >= 2)
        {
            GameObject t=Instantiate(bulletDrone, bulletDronePoint.position, Quaternion.identity);
            if (spriteDrone.flipX==false)
            {
                t.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                t.GetComponent<SpriteRenderer>().flipX = true;
            }
            speedDroneShoot = 0;
            soundShootByDrone.Play();
        }
    }
    //Xử lí va chạm với đạn
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Nếu Tàu va chạm với đạn của Player thì phát nổ
        if (collision.gameObject.CompareTag("Bullet"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            animDrone.SetTrigger("Death");
            soundDeath.Play();
            coll.isTrigger = true;
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.6f);
        }
    }
}
