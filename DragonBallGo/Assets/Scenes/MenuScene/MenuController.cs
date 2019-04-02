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

	public void onGames(){
		Debug.Log("games");
		SceneManager.LoadScene(mScene.GAMES);
	}

	public void onNewGame(){

	}

	IEnumerator ExecuteGetLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return "0";

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield return "1";
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield return "2";
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			Game newGame = new Game();
			newGame.id = "1";
			newGame.lat = Input.location.lastData.latitude.ToString();
			newGame.lng = Input.location.lastData.longitude.ToString();
			newGame.name = "MVD Party";
			newGame.num_players = "1";
			newGame.passphrase = "mvd";
			newGame.radio = "5";
		}

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

	public void onJoinGame(){
		Debug.Log("join game");
	}

	public void onOptions(){
		Debug.Log("options");
		//Destroy(GameObject.Find("Audio"));
		SceneManager.LoadScene(mScene.INIT);
	}
}
