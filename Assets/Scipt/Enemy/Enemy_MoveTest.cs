using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MoveTest : MonoBehaviour
{
    public Transform[] move_point;
    public GameObject Alert;
    public bool detect;
    public float speed;
    public float dt_rangex, dt_rangey, checkDistance;
    public LayerMask jumpGround;
    public Vector2 Boxsize;

    private Enemy_HealTest enemy;
    private Animator ani;
    private Transform player;
    private Rigidbody2D rb;
    private int Index = 0;

    private void Start()
    {
        enemy = GetComponent<Enemy_HealTest>();
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!detect)
        {
            Alert.SetActive(false); // tat canh bao
            if(!ani.GetBool("freeze")) Flip();
            if (Mathf.Abs(transform.position.x - move_point[Index].position.x) < 0.3f) // di chuyen luc khong gap nv
            {
                Index++;
                if (Index >= move_point.Length) Index = 0;
            }

            Vector2 target = new Vector2(move_point[Index].position.x, rb.position.y);
            if (IsGround() && !ani.GetBool("freeze"))
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }
        }
        else
        {
            if (enemy.currentHeal > 0f) Alert.SetActive(true);
            else Alert.SetActive(false);
        }

        if (player.position.x >= move_point[1].position.x - dt_rangex && player.position.x <= move_point[0].position.x + dt_rangex && Mathf.Abs(player.position.y - transform.position.y) <= dt_rangey) detect = true;
        else detect = false;
    }

    void Flip()
    {
        if (Index == 0) transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (transform.position.x < move_point[1].position.x) Index = 0;
        if (Index == 1) transform.rotation = Quaternion.Euler(0, 180, 0);
        if (transform.position.x > move_point[0].position.x) Index = 1;
    }
    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x)       
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);       
        else if (transform.position.x < player.position.x)      
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);       
    }

    public bool IsGround()
    {
        return Physics2D.BoxCast(transform.position, Boxsize, 0f, transform.right, checkDistance, jumpGround);
        // (vi tri, size, angle, huong di chuyen, khoang di chuyen, layer)
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.right * checkDistance, Boxsize);

        Vector2 pivot = new Vector2((move_point[1].position.x + move_point[0].position.x)/2, transform.position.y);
        Vector2 boxsize = new Vector2((move_point[0].position.x - move_point[1].position.x) + 2*dt_rangex, 2*dt_rangey);
        Gizmos.DrawWireCube(pivot, boxsize);
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        if(coll.gameObject.CompareTag("Wall"))
        {
            if (!detect)
            {            
                Index++;
                if (Index >= move_point.Length) Index = 0;
            }
        }    
    }
}
