using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana_Bar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMana(float max_mana)
    {
        slider.maxValue = max_mana;
        slider.value = max_mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }
}
