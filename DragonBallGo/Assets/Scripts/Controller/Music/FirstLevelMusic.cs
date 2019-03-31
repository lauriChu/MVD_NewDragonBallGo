using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelMusic : MonoBehaviour
{
    //Variables to store references to the clips and audiosources from the singleton
    AudioClip firstClip;
    AudioClip randomClip;

    AudioSource[] trackSource;

    //Storing references and start to play
    void Start()
    {
        trackSource = AudioManager.instance.musicSource;

        randomClip = AudioManager.instance.GetRandomClip();
        firstClip = AudioManager.instance.trackList[0];

        if (!trackSource[0].isPlaying)
        {
            if (AudioManager.instance.randomPlay)
            {
               StartCoroutine(AudioManager.instance.FadeIn(trackSource[0], randomClip));

               //AudioManager.instance.PlayMusic(trackSource[0], randomClip, 0f);
               //StartCoroutine(AudioManager.instance.FadeInAudioSource(trackSource[0]));
            }
            else
            {
                StartCoroutine(AudioManager.instance.FadeIn(trackSource[0], firstClip));

                //AudioManager.instance.PlayMusic(trackSource[0], firstClip, 0f);
                //StartCoroutine(AudioManager.instance.FadeInAudioSource(trackSource[0]));
            }
        }
    }   
}