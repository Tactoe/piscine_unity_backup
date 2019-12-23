using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager mm;


    AudioSource src;

    private void Awake()
    {
        if (mm == null)
            mm = this;
    }

    public void ChangeSong(AudioClip newSong)
    {
        src.Stop();
        src.clip = newSong;
        src.loop = false;
        src.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = GameObject.FindWithTag("levelMusic").GetComponent<MusicHolder>().levelMusic;
        src.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
