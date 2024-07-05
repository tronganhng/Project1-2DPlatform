using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    MenuAudio_Manager audioManager;
    public Animator Transition;
    public float transitionTime = 1f;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<MenuAudio_Manager>();    
    }

    public void NextScene()
    {
        audioManager.PlaySFX(audioManager.button);
        StartCoroutine(Loadlevelindex(SceneManager.GetActiveScene().buildIndex + 1));       
    }

    public void BackScene()
    {
        audioManager.PlaySFX(audioManager.button);
        StartCoroutine(Loadlevelindex(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void ExitGame()
    {
        audioManager.PlaySFX(audioManager.button);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Stage2()
    {
        audioManager.PlaySFX(audioManager.button);
        StartCoroutine(Loadlevel("Level2"));
    }

    public void Trainning()
    {
        audioManager.PlaySFX(audioManager.button);
        StartCoroutine(Loadlevel("Turtorial"));
        
    }
    public void Bosstest()
    {
        audioManager.PlaySFX(audioManager.button);
        StartCoroutine(Loadlevel("Boss test"));
        
    }

    public void PlaySFX()
    {
        audioManager.PlaySFX(audioManager.button);
    }

    IEnumerator Loadlevel(string scenename) // Thuc hien chuyen canh roi cho 1 khoang tgian 
    {
        Transition.SetTrigger("change");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scenename);
    }

    IEnumerator Loadlevelindex(int sceneindex) 
    {
        Transition.SetTrigger("change");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneindex);
    }
}
