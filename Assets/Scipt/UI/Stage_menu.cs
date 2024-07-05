using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_menu : MonoBehaviour
{
    [SerializeField] private GameObject stageUI;
    [SerializeField] private Transform check_point;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Vector2.Distance(player.position, check_point.position) <= 1.5f)
        {
            Invoke("NextStage", 1.3f);
        }    
    }

    void NextStage()
    {
        stageUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ToStage2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }
}
