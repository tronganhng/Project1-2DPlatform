using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    AudioManager audio_manager;
    public static bool IsGamePause = false;
    [SerializeField] private GameObject pauseUI, deadUI, winUI;
    private WaterMovement pm;
    Boss_Heal boss;
    public Animator Transition;
    public float transitionTime = 1f;
    private bool canactiveDieUI = true, bosslife = true;

    private void Start()
    {
        audio_manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        pm = FindObjectOfType<WaterMovement>();
        boss = FindObjectOfType<Boss_Heal>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePause) Resume();
            else Pause();
        }

        if (pm.Heal <= 0f && canactiveDieUI)
        {
            canactiveDieUI = false;
            Invoke("DieUI", 2.5f);
        }
        if(boss != null)
        {
            if(boss.currentHeal <= 0 && bosslife){
                bosslife = false;
                Invoke("WinUI", 6.5f);
            }
        }
    }

    public void Pause()
    {
        audio_manager.PlaySFX(audio_manager.button);
        pauseUI.SetActive(true);
        IsGamePause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        audio_manager.PlaySFX(audio_manager.button);
        pauseUI.SetActive(false);
        IsGamePause = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        audio_manager.PlaySFX(audio_manager.button);
        Time.timeScale = 1f;

        StartCoroutine(Loadlevel("Main menu"));
    }

    public void Restart()
    {
        audio_manager.PlaySFX(audio_manager.button);
        Time.timeScale = 1f;
        StartCoroutine(Loadlevelindex(SceneManager.GetActiveScene().buildIndex));
    }

    void DieUI()
    {
        audio_manager.musicSource.Stop();
        audio_manager.PlaySFX(audio_manager.gameover);
        deadUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void WinUI()
    {
        if(winUI != null)
        {
            audio_manager.musicSource.Stop();
            //audio_manager.PlaySFX(audio_manager.gameover);
            winUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ButtonSFX()
    {
        audio_manager.PlaySFX(audio_manager.button);
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
