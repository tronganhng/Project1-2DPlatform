using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Fly_Eye : MonoBehaviour
{
    [SerializeField] private Transform atk_point;
    [SerializeField] private Transform[] detect_point;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float atk_rate, atk_dame, atk_range, range;
    private float AttackTime;
    public AIPath aipath;
    private Animator ani;
    private Transform player;
    private Player_State ps;
    private WaterMovement pm;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ps = player.GetComponent<Player_State>();
        pm = player.GetComponent<WaterMovement>();
    }

    void Update()
    {
        Flip();

        // tat ai khi nv ngoai vung
        if (player.position.x >= detect_point[1].position.x && player.position.x <= detect_point[0].position.x)
        {
            aipath.enabled = true;
        }
        else
        {
            aipath.enabled = false;
        }

        // attack
        if(Vector2.Distance(transform.position, player.position) <= range)
        {
            if(Time.time >= AttackTime && !ps.Inulti && pm.Heal > 0f)
            {
                ani.SetTrigger("atk");
                AttackTime = Time.time + atk_rate;
            }
        }
    }

    void Flip()
    {
        if (aipath.desiredVelocity.x >= .01f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (aipath.desiredVelocity.x <= -.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Atk()
    {
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(atk_point.position, atk_range, playerLayer);

        foreach(Collider2D player in hit_player)
        {
            player.GetComponent<WaterMovement>().TakeDame(atk_dame);
        }
    }

    void DestroyEye()
    {
        Destroy(gameObject);
    }

    /*private void OnDrawGizmosSelected() //ve hitEnemies
    {
        Gizmos.DrawWireSphere(atk_point.position, atk_range);
    }*/
}
