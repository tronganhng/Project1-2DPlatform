using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Heal : MonoBehaviour
{
    public GameObject Ice_Eff, Hit_Eff;
    public Heal_Bar heal_bar;
    public Transform hit_point;
    private Animator ani;

    [SerializeField] private float MaxHeal;
    public float currentHeal;
    private Boss boss;
    GameController audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<GameController>();
        boss = GetComponent<Boss>();
        ani = GetComponent<Animator>();
        currentHeal = MaxHeal;
        heal_bar.SetMaxHeal(MaxHeal);
    }

    public void TakeDame(float Dame)
    {
        if (!boss.inatk)
        {
            currentHeal -= Dame;
            heal_bar.SetHeal(currentHeal);
            ani.SetTrigger("ishurt");
            if (currentHeal <= 0 && !ani.GetBool("isdeath"))
            {
            Die();
            }
        }


    }

    public void SpawnHit_Eff()
    {
        Quaternion rotation = Quaternion.Euler(0,transform.rotation.y*180,Random.Range(-30f, 30f));
        Instantiate(Hit_Eff, hit_point.position, rotation);
    }

    public void SpawnIce_Eff()
    {
        GameObject IceEffect = Instantiate(Ice_Eff, transform.position, transform.rotation);
        IceEffect.transform.SetParent(transform);
    }

    void Die()
    {
        audioManager.musicSource.Stop();
        boss.SFXSource.PlayOneShot(boss.die);
        ani.SetBool("isdeath", true);
    }
}
