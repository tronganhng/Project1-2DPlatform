using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IteamCollect : MonoBehaviour
{
    public GameObject healEff;
    private WaterMovement wm;
    [SerializeField] private float heal_up, mana_up;
    [SerializeField] private Text CoinText;
    public int coin_cnt = 0;
    public Heal_Bar healbar;
    private Player_State ps;
    private Mana_Bar mana_bar;
    AudioManager audioManager;

    private void Start()
    {
        LoadInfor();
        CoinText.text = "" + coin_cnt;
        mana_bar = FindObjectOfType<Mana_Bar>();
        ps = GetComponent<Player_State>();
        wm = FindObjectOfType<WaterMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            audioManager.PlaySFX(audioManager.heal);
            // tao game object = instantiate(healeffect)
            GameObject HE = Instantiate(healEff, transform.position, transform.rotation);
            //set parent cho effect heal la player.tranform
            HE.transform.SetParent(transform);
            Destroy(collision.gameObject);
            wm.Heal += heal_up;
            healbar.SetHeal(wm.Heal);
            if(wm.Heal > wm.maxheal)
            {
                wm.Heal = wm.maxheal;
                healbar.SetMaxHeal(wm.maxheal);
            }
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioManager.PlaySFX(audioManager.coin);
            Destroy(collision.gameObject);
            coin_cnt++;
            CoinText.text = "" + coin_cnt;
        }
        if (collision.gameObject.CompareTag("Mana"))
        {
            audioManager.PlaySFX(audioManager.mana);
            Destroy(collision.gameObject);
            ps.current_mana += mana_up;
            mana_bar.SetMana(ps.current_mana);
            if (ps.current_mana >= ps.Max_mana)
            {
                ps.current_mana = ps.Max_mana;
                mana_bar.SetMaxMana(ps.current_mana);
            }
        }
    }

    private void OnDisable() {
        SaveInfor();
    }

    public void SaveInfor() // luu chi so coin
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadInfor() // load chi so coin
    {
        PlayerData data = SaveSystem.LoadData();
        if(data == null) coin_cnt = 0;
        else coin_cnt = data.Coin_cnt;
    }
}

