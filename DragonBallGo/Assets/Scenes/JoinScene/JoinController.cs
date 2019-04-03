using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JoinController : MonoBehaviour {

	private Games allGames;
	private Games games;
	public GameObject prefabTextButton;
	private VerticalLayoutGroup VerticalLayoutGroup;
	private InputField searchField;
	private Animator Animator;
	private InputField passphraseField;
	private int current;

	// Use this for initialization
	void Start () {
		passphraseField = GameObject.Find("InputField").GetComponent<InputField>();
		Animator = GetComponent<Animator>();
		games = new Games();
		games.games = new List<Game>();
		searchField = GameObject.Find("NameField").GetComponent<InputField>();
		allGames = new Games();
		allGames.games = new List<Game>();
		StartCoroutine(ExecuteGet(Rest.getAllGamesRequest()));
	}
	
	public void onFilter()
	{
		Debug.Log("filtering");
		games.games.Clear();
		if (!string.IsNullOrEmpty(searchField.text)){
			games.games.Clear();
			foreach (var item in allGames.games)
			{
				if (item.name.Contains(searchField.text)){
					games.games.Add(item);
				}
			}
			updateUI();
		} else {
			foreach (var item in allGames.games)
			{
				games.games.Add(item);
			}
			updateUI();
		}
	}
	public void onBack()
	{
		SceneManager.LoadScene(mScene.MENU);
	}

	private void updateUI(){
		VerticalLayoutGroup = GameObject.Find("GameList").GetComponent<VerticalLayoutGroup>();
		RectTransform listRect = VerticalLayoutGroup.GetComponent<RectTransform>();
		foreach (Transform child in listRect)
        {
             Destroy(child.gameObject);
        }
		for (int index = 0; index < games.games.Count; ++index){
			GameObject go = (GameObject)Instantiate(prefabTextButton);
			var a = index;
			go.GetComponent<Button>().onClick.AddListener(() => { onClickGame(a); }); 
			go.GetComponent<TextMeshProUGUI>().text = games.games[index].name;
			go.transform.SetParent(listRect, false);
			if (string.IsNullOrEmpty(games.games[index].passphrase)){
				go.transform.GetChild(0).GetComponent<Image>().enabled = false;
			}
		}
	}
	public void onClickGame(int selected){
		Debug.Log("Clicked game " + selected.ToString());
		current = selected;
		var selectedGame = games.games[selected];
		if (string.IsNullOrEmpty(selectedGame.passphrase))
		{
			// Es una partida pública
			PersistentData.saveSelectedGame(selectedGame.id);
			SceneManager.LoadScene(mScene.MAP);
		} 
		else
		{
			// Es una partida privada
			Debug.Log("privadaaa");
			Animator.SetBool("haveToOpen", true);
		}
	}

	public void onCancel(){
		passphraseField.text = "";
		Animator.SetBool("haveToOpen", false);
	}

	public void onOk(){
		Animator.SetBool("haveToOpen", false);
		if (games.games[current].passphrase == passphraseField.text){
			PersistentData.saveSelectedGame(games.games[current].ToString());
			SceneManager.LoadScene(mScene.MAP);
		}
		passphraseField.text = "";
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
				Debug.Log("Returning:" + request.downloadHandler.text);	
				allGames = JsonUtility.FromJson<Games>(request.downloadHandler.text);
				games = JsonUtility.FromJson<Games>(request.downloadHandler.text);
				updateUI();
			}
		}
	}
}
