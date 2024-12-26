using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source--------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("-------------Audio Clips--------------")]
    public AudioClip background;
    public AudioClip outoftime;
    public AudioClip Bombfail;
    public AudioClip moleclick;

    public static AudioManager instance;
    private void Start()
    {
        // Set up the background music to be ready
        musicSource.clip = background;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Optional: If you want it to persist across scenes
        }

    }
    // Play background music
    public void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    // Stop background music
    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // Play specific sound effects
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    // Specific methods for GameManager events
    public void PlayTimeUpSound()
    {
        PlaySFX(outoftime);
        StopBackgroundMusic();
    }

    public void PlayMoleClickSound()
    {
        PlaySFX(moleclick);
    }

    public void PlayBombFailSound()
    {
        PlaySFX(Bombfail);
        StopBackgroundMusic();
    }
}
