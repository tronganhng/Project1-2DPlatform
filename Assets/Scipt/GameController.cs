using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] characterprefab;
    private Transform player;
    public Animator ChangeCam;
    [SerializeField] GameObject Boss;
    public AudioSource musicSource;
    public AudioClip Boss_bg, Boss_bg2;
    [SerializeField] private Transform Stage_Point2, spawn_point;
    [SerializeField] private float nextstage_rate; // vi tri la tgian delay khi qua man
    private float time;
    private bool boss_appear;

    AudioManager audioManager;
    WaterMovement playerheal;

    private void Awake()
    {
        int CharacterIndex = PlayerPrefs.GetInt("CharacterIndex");
        GameObject playerpefab = characterprefab[CharacterIndex];
        Instantiate(playerpefab, spawn_point.position, Quaternion.identity);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
         
    }

    void Start()
    {
        Boss.SetActive(false);
        time = nextstage_rate;
        player = GameObject.FindGameObjectWithTag("Player").transform;    
        playerheal = player.GetComponent<WaterMovement>();
    }

    
    void Update()
    {
        if (Vector2.Distance(player.position, Stage_Point2.position) <= 1.8f)
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0f)
            {
                time = nextstage_rate;
                ChangeCam.SetTrigger("change");
                Invoke("ChangePos", 0.8f);
            }
        }

        if (player.position.y >= 72f && player.position.y <= 77f && !boss_appear) // neu player trong vung boss thi active boss
        {
            time -= Time.fixedDeltaTime;
            if(time <= 0f)
            {
                audioManager.musicSource.Stop();
                musicSource.clip = Boss_bg;
                musicSource.Play(); 
                time = nextstage_rate;
                boss_appear = true;
                Boss.SetActive(true);
            }
        }
        if(playerheal.Heal <= 0) musicSource.Stop();
    }

    void ChangePos()
    {
        player.position = new Vector3(112.38f, 84.02f, 0f);
    }
}
