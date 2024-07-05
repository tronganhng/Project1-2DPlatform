using UnityEngine;

public class MenuAudio_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip background;
    public AudioClip button;

    private void Start() {
       musicSource.clip = background; // gan sound cua musicSource la bg sound clip
       musicSource.loop = true;
       musicSource.Play();  // phat nhac
    }

    public void PlaySFX(AudioClip SFX)
    {
        SFXSource.PlayOneShot(SFX); // chay audio clip 1 lan
        
    }
}
