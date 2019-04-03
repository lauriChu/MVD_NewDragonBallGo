using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMapController : MonoBehaviour {

	private Animator Animator;
	// Use this for initialization
	void Start () {
		Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onOpenMenu()
	{
		Debug.Log("Menu Open");
		if (!Animator.GetBool("haveToOpenMenu") && Animator.GetBool("haveToCloseMenu")){
			Animator.SetBool("haveToOpenMenu", true);
			Animator.SetBool("haveToCloseMenu", false);
		}
		else
		{
			Animator.SetBool("haveToOpenMenu", false);
			Animator.SetBool("haveToCloseMenu", true);
		}
	}

	public void onOpenUser()
	{
		Debug.Log("open user scene");
	}
}
