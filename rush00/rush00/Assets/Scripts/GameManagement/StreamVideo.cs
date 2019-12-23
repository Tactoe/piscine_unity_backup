using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class StreamVideo : MonoBehaviour
{
    // Start is called before the first frame update
    public Image cover;
    public AudioSource audioSrc;
    public RawImage[] imgs;
    public VideoPlayer vid;

    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        vid.Prepare();
        while (!vid.isPrepared)
        {
            yield return new WaitForSeconds(1);
        }
        if (cover != null && audioSrc != null)
        {
            cover.gameObject.SetActive(false);
            audioSrc.Play();
        }
        foreach (RawImage img in imgs)
            img.texture = vid.texture;
        vid.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
