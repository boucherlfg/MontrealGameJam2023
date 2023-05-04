using UnityEngine;

public class Music : SingletonBehaviour<Music>
{
    [SerializeField]
    private AudioSource audioSource;

    public AudioClip menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Change(AudioClip clip)
    {
        if (clip == audioSource.clip) return;
        var position = audioSource.time;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.time = position;
        audioSource.Play();
    }
}
