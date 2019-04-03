using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
public class LoginController : MonoBehaviour {
	private Text userField;
	private Text passwordField;
	private InputField passwordInputField;
	private TextMeshProUGUI infoText;

	// Use this for initialization
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
		LoadingManager.Shared.Show(SceneManager.LoadSceneAsync(mScene.INIT));
	}
	public void onLogin() 
	{
		if (string.IsNullOrEmpty(userField.text) || string.IsNullOrEmpty(passwordField.text))
		{
			Debug.Log("Empty fields");
			infoText.text = "Complete fields";
		}else{
			Debug.Log("Doing Login");
			infoText.text = "";
			LoadingManager.Shared.Show(SceneManager.LoadSceneAsync(mScene.MENU));
			//StartCoroutine(ExecuteGet(Rest.getLoginRequest(userField.text, passwordInputField.text)));
		}
	} 

	IEnumerator ExecuteGet(UnityWebRequest request)
	{
		yield return request.Send();
	
		if (request.isNetworkError)
		{				
			Debug.Log(request.error);
		}
		else
		{
			if (request.responseCode == 200) {
				// Show results as text
				Debug.Log(request.responseCode);
				Debug.Log("Returning:" + request.downloadHandler.text);	
				User user = JsonUtility.FromJson<User>(request.downloadHandler.text);
				PersistentData.saveUserPrefs(user);
				SceneManager.LoadScene(mScene.MENU);
			} else {
				infoText.text = "Error ocurred";
			}
		}
	}
}
