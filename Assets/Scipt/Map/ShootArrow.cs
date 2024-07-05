using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private Animator ani;
    [SerializeField] private float ArrowTime, Time_Rate; //ArrowTime: bien tgian chay ; Time_Rate: tgian 2 lan ban
    Transform player;
    TrapAudio auido;
    void Start()
    {
        auido = GetComponentInChildren<TrapAudio>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time >= ArrowTime && Vector2.Distance(transform.position,player.position) <= 18f)
        {
            ani.SetTrigger("active");
            auido.PlaySFX(auido.active);
            ArrowTime = Time.time + Time_Rate;
        }        
    }

    void Spawn_Arrow()
    {
        Instantiate(arrow, transform.position, transform.rotation);
    }
}
