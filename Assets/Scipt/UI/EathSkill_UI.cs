using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EathSkill_UI : MonoBehaviour
{
    public Image skill1_image, skill2_image, skill3_image;
    public float cooldown1, cooldown2, cooldown3;
    bool isCooldown = false, isCooldown2 = false, isCooldown3 = false;

    private EarthCombat pa;

    private void Awake()
    {
        pa = FindObjectOfType<EarthCombat>();
    }
    void Start()
    {
        cooldown1 = pa.bump_rate;
        cooldown2 = pa.shield_rate;
        cooldown3 = pa.ulti_rate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            skill1_image.fillAmount += 1 / cooldown1 * Time.deltaTime;
            if (skill1_image.fillAmount >= 1)
            {
                skill1_image.fillAmount = 1f;
                isCooldown = false;
            }
        }

        if (isCooldown2)
        {
            skill2_image.fillAmount += 1 / cooldown2 * Time.deltaTime;
            if (skill2_image.fillAmount >= 1)
            {
                skill2_image.fillAmount = 1f;
                isCooldown2 = false;
            }
        }

        if (isCooldown3)
        {
            skill3_image.fillAmount += 1 / cooldown3 * Time.deltaTime;
            if (skill3_image.fillAmount >= 1)
            {
                skill3_image.fillAmount = 1f;
                isCooldown3 = false;
            }
        }
    }

    public void Ability1()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            skill1_image.fillAmount = 0f;
        }
    }
    public void Ability2()
    {
        if (!isCooldown2)
        {
            isCooldown2 = true;
            skill2_image.fillAmount = 0f;
        }
    }
    public void Ability3()
    {
        if (!isCooldown3)
        {
            isCooldown3 = true;
            skill3_image.fillAmount = 0f;
        }
    }        
}
