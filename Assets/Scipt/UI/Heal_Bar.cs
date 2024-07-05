using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal_Bar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;  // khoang gia tri (0,1)
    public void SetMaxHeal(float max_heal)
    {
        slider.maxValue = max_heal;
        slider.value = max_heal;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHeal(float heal)
    {
        slider.value = heal;

        fill.color = gradient.Evaluate(slider.normalizedValue); //lay slider value tu 0->1
    }
}
