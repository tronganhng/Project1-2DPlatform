using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private float spearTime = 0f; // bien tgian chay
    [SerializeField] private float Time_Rate; // tgian 2 lan active trap
    [SerializeField] private float dame;
    private Animator ani;
    TrapAudio auido;
    Transform player;
    void Start()
    {
        auido = GetComponentInChildren<TrapAudio>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time >= spearTime)
        {
            ani.SetTrigger("active");
            spearTime = Time.time + Time_Rate;
            if(Vector2.Distance(transform.position,player.position) <= 18f)
                auido.PlaySFX(auido.active);
        }
    }

    
}
