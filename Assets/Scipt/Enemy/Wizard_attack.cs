using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_attack : MonoBehaviour
{
    private Animator ani;
    [SerializeField] private float atk_rate;
    [SerializeField] private Transform atk_point;
    [SerializeField] private GameObject Bullet;
    private float AttackTime;
    private WaterMovement pm;
    private Enemy_MoveTest emove;
    EnemyAudio audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<EnemyAudio>();
        emove = GetComponent<Enemy_MoveTest>();
        pm = FindObjectOfType<WaterMovement>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (ani.GetBool("stop") && emove.detect && !ani.GetBool("freeze"))
        {
            if(Time.time >= AttackTime && pm.Heal > 0)
            {
                ani.SetTrigger("atk");
                AttackTime = Time.time + Random.Range(atk_rate - 0.35f, atk_rate + 0.35f);
            }
        }  
    }

    void SpawnBullet()
    {
        audioManager.PlaySFX(audioManager.bullet);
        Instantiate(Bullet, atk_point.position, atk_point.rotation);
    }
}
