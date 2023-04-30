using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music makeUnique;
    private bool inGame;
    private float position;
    private int lastIndex = -1;
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public bool followEpochs;

    public AudioClip[] epochMusic;
    public AudioClip menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        if (!makeUnique) makeUnique = this;
        else Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        position = audioSource.time;
        if (followEpochs)
        {
            inGame = true;
            var index = Gamestate.Instance.EnvironmentIndex;
            if (index == lastIndex) return;

            lastIndex = index;
            SwiftMusicChange(epochMusic[index]);
        }
        else 
        {
            if (!inGame) return;
            inGame = false;
            lastIndex = -1;
            SwiftMusicChange(menuMusic);
        }
    }
    void SwiftMusicChange(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.time = position;
        audioSource.Play();
    }
}
