using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private float close_time = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        if(Mathf.Abs(player.position.y - transform.position.y) < 9f)
        {
            if(player.position.x > transform.position.x + 2f)
            {
                close_time -= Time.deltaTime;
                rb.velocity = new Vector2(0, -4);
                if(close_time <= 0f)
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }
        }    
    }
}
