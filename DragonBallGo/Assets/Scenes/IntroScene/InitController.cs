using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class InitController : MonoBehaviour {

	// Use this for initialization
	public AudioSource audio;
	void Start () {
		
	}


 void Awake()
 {
                     
    audio.Play ();
	DontDestroyOnLoad (audio);
                                 
 }
 void Update () {
     
 }
	public void onClickLogin() 
	{
		Debug.Log("login");
		SceneManager.LoadScene(mScene.LOGIN);
	}

	public void onClickRegister()
	{
		Debug.Log("Register");
		SceneManager.LoadScene(mScene.REGISTER);
	}

	public void onClickOptions()
	{
		Debug.Log("Options");
	}
	
	
}
