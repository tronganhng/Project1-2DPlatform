using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource, mapSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip music_bg, map_bg;
    public AudioClip button;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip coin, mana, heal;
    public AudioClip gameover;

    private void Start() {
        musicSource.clip = music_bg;
        musicSource.loop = true;
        musicSource.Play();

        if(mapSource != null)
        {
            mapSource.clip = map_bg;
            mapSource.loop = true;
            mapSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
