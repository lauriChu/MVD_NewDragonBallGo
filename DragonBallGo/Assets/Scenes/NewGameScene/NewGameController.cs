using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour {

	public float Lat;
	public float Lng;
	private InputField nameField;
	private InputField passphraseField;
	private InputField distanceField;
	private InputField playersField;
	private TextMeshProUGUI infoText;

	// Use this for initialization
	void Start () {
		StartCoroutine(GetLocation());
		nameField = GameObject.Find("NameField").GetComponent<InputField>();
		passphraseField = GameObject.Find("PassField").GetComponent<InputField>();
		distanceField = GameObject.Find("DistanceField").GetComponent<InputField>();
		playersField = GameObject.Find("PlayerField").GetComponent<InputField>();
		infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onCreate() {
		if (string.IsNullOrEmpty(nameField.text) || 
			string.IsNullOrEmpty(distanceField.text) || 
			string.IsNullOrEmpty(playersField.text)) 
		{
			infoText.text = "Complete required fields";
		}
		else
		{
			infoText.text = "";
			Game game = new Game();
			game.lat = Lat.ToString();
			game.lng = Lng.ToString();
			game.name = nameField.text;
			game.radio = distanceField.text;
			game.num_players = playersField.text;
			game.passphrase = passphraseField.text;
			string parameters = JsonUtility.ToJson(game) ?? "";
			StartCoroutine(ExecutePost(Rest.postNewGame(parameters)));
		}
	}

	public void onBack() {
		SceneManager.LoadScene(mScene.MENU);
	}
	IEnumerator ExecutePost(UnityWebRequest request)
	{
		yield return request.Send();

		if (request.isNetworkError)
		{
			Debug.Log(request.error);
		}
		else 
		{
			if (request.responseCode == 200) {
				// Show results
				Debug.Log("Returning:" + request.downloadHandler.text);
				Debug.Log("to Main another time");

				SceneManager.LoadScene(mScene.MENU);
			} else {
				infoText.text = "Error ocurred";
			}
		}
	}

	IEnumerator GetLocation (){
		print("Start Location");
		// First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
			print("Not enabled");
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
            yield break;
        }

		// Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			Lat = Input.location.lastData.latitude;
			Lng = Input.location.lastData.longitude;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
	}
}
