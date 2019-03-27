using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void aaaa()
	{
		Debug.Log("me cago en la puta!");	
	}

	public void onGames(){
		Debug.Log("games");
		SceneManager.LoadScene(mScene.GAMES);
	}

	public void onNewGame(){
		Debug.Log("new game");
	}

	public void onJoinGame(){
		Debug.Log("join game");
	}

	public void onOptions(){
		Debug.Log("options");
		Destroy(GameObject.Find("Audio"));
		SceneManager.LoadScene(mScene.INIT);
	}
}
