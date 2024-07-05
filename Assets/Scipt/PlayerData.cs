using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Coin_cnt = 0;

    public PlayerData(IteamCollect player)
    {
        Coin_cnt = player.coin_cnt; // luu chi so coin
    }     
}
