using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HealTest : MonoBehaviour
{
    public GameObject Enemy, Ice_Eff, Hit_Eff, Blood, Chunk, Coin, Pop_Text;
    public Transform hit_point, coin_point;
    public Heal_Bar heal_bar;
    public float MaxHeal, currentHeal;

    private Animator ani;
    private Player_State ps;
    EnemyAudio audioManager;
    CameraShake mycamera;

    private void Start()
    {
        mycamera = FindObjectOfType<CameraShake>();
        audioManager = FindObjectOfType<EnemyAudio>();
        ps = FindObjectOfType<Player_State>();
        ani = GetComponent<Animator>();
        currentHeal = MaxHeal;
        heal_bar.SetMaxHeal(MaxHeal);
    }

    public void TakeDame(float dame)
    {
        if (ps.Inulti)
        {
            ani.SetBool("shield", false);
            currentHeal -= dame;
            GameObject point = Instantiate(Pop_Text, coin_point.position, Quaternion.identity);
            point.transform.GetChild(0).GetComponent<TextMesh>().text = "" + dame;
            heal_bar.SetHeal(currentHeal);
            ani.SetTrigger("ishurt");
            if (currentHeal <= 0)
            {
                Die();
            }
        }
        else
        {
            if (!ani.GetBool("shield"))
            {
                if(audioManager.takehit != null)
                    audioManager.PlaySFX(audioManager.takehit);
                currentHeal -= dame;
                GameObject point = Instantiate(Pop_Text, coin_point.position, Quaternion.identity);
                point.transform.GetChild(0).GetComponent<TextMesh>().text = "" + dame;
                heal_bar.SetHeal(currentHeal);
                ani.SetTrigger("ishurt");
                if (currentHeal <= 0)
                {
                    Die();
                }
            }
            else audioManager.PlaySFX(audioManager.hit_shield);
        }
    }
    void Die()
    {
        ani.SetBool("isdeath", true);
    }

    public void BloodSplash()
    {
        audioManager.PlaySFX(audioManager.blood);
        if(Blood != null && Chunk != null)
        {
            Instantiate(Blood, hit_point.position, Quaternion.identity);
            Instantiate(Chunk, hit_point.position, Quaternion.identity);
        }
        if(mycamera != null)
            mycamera.Shake(2.3f, 0.2f);
        //Destroy(blood, 3f);
    }

    public void SpawnIce_Eff()
    {
        GameObject IceEffect = Instantiate(Ice_Eff, transform.position, transform.rotation);
        IceEffect.transform.SetParent(transform);
    }

    public void SpawnHit_Eff(GameObject hitEff)
    {
        Quaternion rotation = Quaternion.Euler(0,transform.rotation.y*180,Random.Range(-30f, 30f));
        Instantiate(hitEff, hit_point.position, rotation);
    }

    void DestroyEnemy()
    {
        Destroy(Enemy);
    }

    void SpawnCoin()
    {
        int random = Random.Range(1, 11);
        if (random > 3) Instantiate(Coin, coin_point.position, coin_point.rotation); // ti le rot coin 70%
        else return;
    }
}
