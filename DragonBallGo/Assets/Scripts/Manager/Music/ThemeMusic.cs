using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeMusic : MonoBehaviour
{
    AudioClip audioClip;
    AudioSource[] trackSource;

    // Start is called before the first frame update
    void Start()
    {
        trackSource = AudioManager.shared.musicSource;
        audioClip = AudioManager.shared.themeClip;

        if(!trackSource[0].isPlaying)
        {
            StartCoroutine(AudioManager.shared.FadeIn(trackSource[0], audioClip));
        }
    }
}
