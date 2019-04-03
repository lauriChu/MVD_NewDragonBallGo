using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class GamesController : MonoBehaviour {

	public GameObject prefabTextButton;
	private VerticalLayoutGroup VerticalLayoutGroup;
	private Games games;
	void Start () {
		User user = PersistentData.getUserPrefs();
		Debug.Log(user.name);
		StartCoroutine(ExecuteGet(Rest.getGamesRequest()));
	}

	private void updateUI(){
		VerticalLayoutGroup = GameObject.Find("GameList").GetComponent<VerticalLayoutGroup>();
		RectTransform listRect = VerticalLayoutGroup.GetComponent<RectTransform>();
		for (int index = 0; index < games.games.Count; ++index){
			GameObject go = (GameObject)Instantiate(prefabTextButton);
			var a = index;
			go.GetComponent<Button>().onClick.AddListener(() => { onClickGame(a); }); 
			go.GetComponent<TextMeshProUGUI>().text = games.games[index].name;
			go.transform.SetParent(listRect, false);
		}
	}

	public void onClickGame(int selected){
		Debug.Log("Clicked game " + selected.ToString());
		PersistentData.saveSelectedGame(selected.ToString());
		SceneManager.LoadScene(mScene.MAP);
	}
	public void onBack()
	{
		LoadingManager.Shared.Show(SceneManager.LoadSceneAsync(mScene.MENU));
	}
	
	// Update is called once per frame
	void Update () {
		
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
			// Show results as text
			Debug.Log("Returning:" + request.downloadHandler.text);	
			games = JsonUtility.FromJson<Games>(request.downloadHandler.text);
			updateUI();
		}
	}
}
