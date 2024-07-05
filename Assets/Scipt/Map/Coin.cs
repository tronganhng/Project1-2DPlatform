using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Vector2 direct = new Vector2(-transform.right.x * 1.5f, 3f); // luc rot xu ban len
        rb.velocity = speed * direct;
    }
}
