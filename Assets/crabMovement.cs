using UnityEngine;

public class crabMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    public GameObject player;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    void Update()
    {
       rb.velocity = player.transform.position * speed;
    }
}
