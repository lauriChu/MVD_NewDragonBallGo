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
        trackSource = AudioManager.shared.musicSource;

        randomClip = AudioManager.shared.GetRandomClip();
        firstClip = AudioManager.shared.trackList[0];

        if (!trackSource[0].isPlaying)
        {
            if (AudioManager.shared.randomPlay)
            {
               StartCoroutine(AudioManager.shared.FadeIn(trackSource[0], randomClip));

               //AudioManager.instance.PlayMusic(trackSource[0], randomClip, 0f);
               //StartCoroutine(AudioManager.instance.FadeInAudioSource(trackSource[0]));
            }
            else
            {
                StartCoroutine(AudioManager.shared.FadeIn(trackSource[0], firstClip));

                //AudioManager.instance.PlayMusic(trackSource[0], firstClip, 0f);
                //StartCoroutine(AudioManager.instance.FadeInAudioSource(trackSource[0]));
            }
        }
    }   
}