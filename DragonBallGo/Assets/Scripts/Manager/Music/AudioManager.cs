﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Variables for music audiosources 
    public static AudioManager instance = null;

    public bool randomPlay = false;
    
    public AudioSource[] musicSource;
    public AudioClip[] trackList;

    public AudioMixerGroup track01;
    public AudioMixerGroup track02;

    int clipOrder = 0;

    public float fadeInTime = 2.0f;
    public float fadeOutTime = 2.0f;

    //Awake: we create the instance of the singleton and both audiosources for music

    private void Awake()
    {
        //Create Singleton
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        /*Create audiosources for music and direct them to the audiomixer linked groups 
        (On Awake so other scripts can access them from Start)*/

        musicSource = new AudioSource[2];

        musicSource[0] = gameObject.AddComponent<AudioSource>();
        musicSource[0].outputAudioMixerGroup = track01;

        musicSource[1] = gameObject.AddComponent<AudioSource>();
        musicSource[1].outputAudioMixerGroup = track02;
    }

    
    public void PlayMusic(AudioSource source, AudioClip clip, float volume)
    {
        source.volume = volume;
        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    //Method for crossfading between random music clips in the list using the available audiosource
    public void CrossfadeRandomMusic()
    {
        if (!musicSource[1].isPlaying)
        {
            StartCoroutine(FadeOut(musicSource[0]));
            StartCoroutine(FadeIn(musicSource[1], GetRandomClip()));
        }
        else
        {
            StartCoroutine(FadeOut(musicSource[1]));
            StartCoroutine(FadeIn(musicSource[0], GetRandomClip()));
        }
    }

    //Method for crossfading with the next music clip in the list using the available audiosource
    public void CrossfadeNextMusic()
    {
        if (!musicSource[1].isPlaying)
        {
            StartCoroutine(FadeOut(musicSource[0]));
            StartCoroutine(FadeIn(musicSource[1], GetNextClip()));
        }
        else
        {
            StartCoroutine(FadeOut(musicSource[1]));
            StartCoroutine(FadeIn(musicSource[0], GetNextClip()));
        }
    }

    //Method for getting a random cmusic clip from the list
    public AudioClip GetRandomClip()
    {
        return trackList[Random.Range(0, trackList.Length)];
    }

    //Method for getting the next clip in the list
    public AudioClip GetNextClip()
    {
        if (clipOrder >= trackList.Length - 1)
        {
            clipOrder = 0;
        }
        else
        {
            clipOrder += 1;
        }
        return trackList[clipOrder];
    }

    //Fade In a particular clip
    public IEnumerator FadeIn(AudioSource source, AudioClip clip)
    {
        source.volume = 0.0f;
        source.clip = clip;
        source.loop = true;
        source.Play();

        while (source.volume < 1.0f)
        {
            source.volume += 1 * Time.deltaTime / fadeInTime;
            yield return null;
        }
        source.volume = 1.0f;
    }

    //Fade Out the audiosource we choose
    public IEnumerator FadeOut(AudioSource source)
    {
        source.volume = 1f;

        while (source.volume > 0.001f)
        {
            source.volume -= 1 * Time.deltaTime / fadeOutTime;
            yield return null;
        }
        source.volume = 0.0f;
        source.Stop();
    }

    //Fade In the audiosource we choose
    public IEnumerator FadeInAudioSource(AudioSource source)
    {
        while (source.volume < 1.0f)
        {
            source.volume += 1 * Time.deltaTime / fadeInTime;
            yield return null;
        }
        source.volume = 1.0f;
        source.loop = true;
    }
}