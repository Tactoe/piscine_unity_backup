using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set; }
    // Start is called before the first frame update
    private AudioSource src;
    void Awake()
    {
        instance = this;
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip toPlay)
    {
        src.clip = toPlay;
        src.Play();
    }
}
