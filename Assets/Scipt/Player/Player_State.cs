using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    public bool Inatk, Inulti, Incall;
    public float current_mana, Max_mana;

    private void Start()
    {
        current_mana = Max_mana;
    }
}
