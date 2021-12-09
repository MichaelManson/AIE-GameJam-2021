using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static SoundManager Instance;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }

        Instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    #endregion

    #region Fields
    
    // private:
    
    [SerializeField]
    private AudioSource musicSource;
    
    #endregion
    
    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource, float delay = 0f)
    {
        audioSource.clip = audioClip;
        audioSource.Play((ulong)delay);
    }

    public void PlayMusic(AudioClip song)
    {
        musicSource.clip = song;
        musicSource.Play();
    }
}
