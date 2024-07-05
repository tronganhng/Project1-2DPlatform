using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private WaterMovement pm;
    private Animator ani;
    [SerializeField] private float speed, dame;
    private Rigidbody2D rb;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        pm = FindObjectOfType<WaterMovement>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = transform.up * -speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(0, 0);
            ani.SetTrigger("explode");
        }
        if (collision.gameObject.CompareTag("Player") && pm.Heal > 0f)
        {
            rb.velocity = new Vector2(0, 0);
            ani.SetTrigger("explode");
            pm.TakeDame(dame);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
