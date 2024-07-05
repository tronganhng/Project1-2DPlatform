using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;
    public AudioClip step;
    public AudioClip hurt, death;
    public AudioClip melee;
    public AudioClip atk3;
    public AudioClip uskill, kskill;
    public AudioClip ulti1;
    public AudioClip ulti2;
    public AudioClip hit_enemy;
    EnemyAudio enemyAudio;

    void Awake()
    {
        enemyAudio = FindObjectOfType<EnemyAudio>();
        if(enemyAudio != null) enemyAudio.takehit = hit_enemy;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
