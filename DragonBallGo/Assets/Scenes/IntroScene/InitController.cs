using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class InitController : MonoBehaviour {

	// onClick Listener for Login button
	public void onClickLogin() 
	{
		Debug.Log("Login");
		LoadingManager.Shared.Show(SceneManager.LoadSceneAsync(mScene.LOGIN));
	}

	public void onClickRegister()
	{
		Debug.Log("Register");
		LoadingManager.Shared.Show(SceneManager.LoadSceneAsync(mScene.REGISTER));
	}

	public void onClickOptions()
	{
		Debug.Log("Options");
	}
	
	
}
