using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void Loop(bool isLoop)
    {
        source.loop = isLoop;
    }

    public void Play()
    {
        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Remove()
    {
        source.enabled = false;
    }

    public AudioPlayer Source(AudioClip src)
    {
        source.clip = src;
        return this;
    }
}
