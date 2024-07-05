using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controll : MonoBehaviour
{
    public AudioSource ghostmusic;
    public AudioClip background;
    public GameObject[] characterprefab;
    public Transform Enemy_cnt;
    public Animator Transition;
    [SerializeField] private Transform stage_point, spawn_point;
    [SerializeField] private float nextstage_rate;
    private float time;
    private Transform player;
    private bool donesStage;
    private int childcnt;
    AudioManager audioManager;

    private void Awake()
    {
        int CharacterIndex = PlayerPrefs.GetInt("CharacterIndex");
        GameObject playerpefab = characterprefab[CharacterIndex];
        Instantiate(playerpefab, spawn_point.position, Quaternion.identity);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        childcnt = -1;
        time = nextstage_rate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy_cnt.childCount <= 0 && childcnt <= 0)
        {
            donesStage = true;
            childcnt = 4;
        }
        if (donesStage)
        {
            donesStage = false;
            Transition.SetTrigger("change");
        }
        if(Vector2.Distance(player.position, stage_point.position) <= 1.5f)
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0f)
            {
                time = nextstage_rate;
                Transition.SetTrigger("change");
                Invoke("ChangePos", 0.8f);
            }
        }
    }

    void ChangePos()
    {
        audioManager.musicSource.Stop();
        ghostmusic.clip = background;
        ghostmusic.Play();
        player.position = new Vector3(204.6f, -51.9f, 0f);
    }

}
