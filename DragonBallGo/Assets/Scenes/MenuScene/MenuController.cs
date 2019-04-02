using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {

	private Game newGame;
	private bool status = false;
	private string error;

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
		Debug.Log("games");
		//StartCoroutine(ExecuteGetLocation());
		navigateToNewGame();
	}

	private void navigateToNewGame() {
		SceneManager.LoadScene(mScene.MAP);
	}

	IEnumerator ExecuteGetLocation()
    {
		
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            status = false;
			error = "Enable location";
			Debug.Log(error);
            yield break;

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
			status = false;
			error = "Timed out";
			Debug.Log(error);
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            status = false;
			error = "Timed out";
			Debug.Log(error);
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			newGame = new Game();
			newGame.id = "1";
			newGame.lat = Input.location.lastData.latitude.ToString();
			newGame.lng = Input.location.lastData.longitude.ToString();
			newGame.name = "MVD Party";
			newGame.num_players = "1";
			newGame.passphrase = "mvd";
			newGame.radio = "5";

			status = true;
			navigateToNewGame();
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
