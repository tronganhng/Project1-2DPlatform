using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;
    public AudioClip explode;
    public AudioClip explode2;
    public AudioClip melee;
    public AudioClip bullet;
    public AudioClip blood, takehit, hit_shield;

    public void PlaySFX(AudioClip clip)
    {
        if(clip == explode){
            SFXSource.volume = 0.6f;
        }
        else SFXSource.volume = 1;
        SFXSource.PlayOneShot(clip);
    }
}
