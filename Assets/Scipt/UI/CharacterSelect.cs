using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] character;
    public int CharacterIndex = 0;


    public void NextCharacter()
    {
        character[CharacterIndex].SetActive(false);
        CharacterIndex = (CharacterIndex + 1) % character.Length;
        character[CharacterIndex].SetActive(true);
    }

    public void BackCharacter()
    {
        character[CharacterIndex].SetActive(false);
        CharacterIndex--;
        if (CharacterIndex < 0) CharacterIndex = character.Length - 1;
        character[CharacterIndex].SetActive(true);
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharacterIndex", CharacterIndex);
    }
}
