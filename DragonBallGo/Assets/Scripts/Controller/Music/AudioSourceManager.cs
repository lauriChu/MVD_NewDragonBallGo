using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceManager : MonoBehaviour {

    public AudioClip[] clip;
    public AudioMixerGroup sfxGroup;

    AudioSource audiosource;

    [Range (0f, 1f)]
    public float volume = 1;

    public bool randomPitch = false;
    public float minPitch;
    public float maxPitch;

    [Range (0.25f, 3f)]
    public float pitch = 1;
  
    [Range (0f, 1f)]
    public float spatialBlend = 0f;

    [Range (0f, 5f)]
    public float doppler = 0f;

    [Range (0f, 360f)]
    public float spread = 0;

    public int maxDistance;
		
	// Update is called once per frame

	void Start ()
    {
        //Whatever Input is applied here (or through a custom method called from an external event)
		PlayAudioSource();
        
	}

    //We get an audiosource from the pool and we attach it to the object where the script is
    void PlayAudioSource()
    {        
        GameObject obj = AudioSourcePool.current.GetPooledObject();
        
        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        audiosource = obj.GetComponent<AudioSource>();


        //We establish the parameters stored in the public variables
        SetSourceProperties(volume, pitch, spatialBlend, doppler, maxDistance);

        //Choose a Random clip from the list and play the audiosource
        AudioClip pickSFX = clip[Random.Range(0, clip.Length)];
        audiosource.clip = pickSFX;

        if (randomPitch)
            audiosource.pitch = Random.Range(minPitch, maxPitch);

        audiosource.outputAudioMixerGroup = sfxGroup;
        audiosource.Play();
        
        //After the clip has played completely, we return the object to the pool
        StartCoroutine(DestroyAudioSource(obj, pickSFX));
    }

    //Define the audiosource properties through a custom method
    public void SetSourceProperties(float _volume, float _pitch, float _spatialblend, float _dopplerLevel, float _maxdistance)
    {       
        audiosource.volume = _volume;
        audiosource.spatialBlend = _spatialblend;
        audiosource.dopplerLevel = _dopplerLevel;
        audiosource.rolloffMode = AudioRolloffMode.Linear;
        audiosource.maxDistance = _maxdistance;
    }

    //Courutine for returning the audiosource to the pool
    IEnumerator DestroyAudioSource(GameObject gameobject, AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        audiosource.Stop();

        gameobject.SetActive(false);
    }

}
