using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class RockPaperScissorController : MonoBehaviour {

	private int rock;

	private int paper;

	private int scissor;

	public TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
		rock = 0;
		paper = 0;
		scissor = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void isCompleted(){
		if (rock + paper + scissor == 3)
		{
			if (rock == 2 && paper == 2){
				text.text = "WIN!";
				StartCoroutine(WIN());

			}else{
				text.text = "YOU LOSE!";
				StartCoroutine(LOSE());
			}
		}
	}

	IEnumerator WIN()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(mScene.MAP);
	}

	IEnumerator LOSE()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(mScene.MAP);
	}

	public void onRock()
	{
		rock++;
		isCompleted();
	}

	public void onPaper()
	{
		paper++;
		isCompleted();
	}

	public void onScissor()
	{
		scissor++;
		isCompleted();
	}
}
