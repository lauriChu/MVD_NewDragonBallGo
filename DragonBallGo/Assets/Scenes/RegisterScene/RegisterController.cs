using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class RegisterController : MonoBehaviour {

	private Text userField;
	private Text passwordField;
	private TextMeshProUGUI infoText;
	private InputField passwordInputField;
	void Start () {
		userField = GameObject.Find("UserText").GetComponent<Text>();
		passwordField = GameObject.Find("PasswordText").GetComponent<Text>();
		passwordInputField = GameObject.Find("PasswordField").GetComponent<InputField>();
		infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onBack()
	{
		Destroy(GameObject.Find("Audio"));
		SceneManager.LoadScene(mScene.INIT);
	}

	public void onRegister() 
	{
		if (string.IsNullOrEmpty(userField.text) || string.IsNullOrEmpty(passwordField.text))
		{
			Debug.Log("Empty fields");
			infoText.text = "Complete all fields";
		}else{
			Debug.Log("Doing Login");
			User user = new User();
			user.username = userField.text;
			user.name = userField.text;
			user.password = passwordInputField.text;
			string parameters = JsonUtility.ToJson(user) ?? "";
			
			infoText.text = "You are being registered, please wait few seconds";

			StartCoroutine(ExecutePost(Rest.postNewUser(parameters)));
		}
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

				SceneManager.LoadScene(mScene.LOGIN);
			} else {
				infoText.text = "Error ocurred";
			}
		}
	}
}
