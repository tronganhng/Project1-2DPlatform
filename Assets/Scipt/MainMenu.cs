using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text CoinTxt;

    private float coin_cnt = 0;
    void Start()
    {        
        LoadInfor();
        CoinTxt.text = "" + coin_cnt; 
    }

    public void LoadInfor() // load chi so coin
    {
        PlayerData data = SaveSystem.LoadData();
        if(data == null) coin_cnt = 0;
        else coin_cnt = data.Coin_cnt;
    }
}
