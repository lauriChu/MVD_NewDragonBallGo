using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Rest {

	// URL's
	private static string URL_OLD = "http://ec2-52-15-203-194.us-east-2.compute.amazonaws.com/v1/";
	private static string URL = "https://ddcd6db7-f958-4035-bc28-0b095332c0b5.mock.pstmn.io/";
	public static string getGamesUrl(string userId)
	{
		return URL + "games/" + userId;
	}
	public static string getLoginUrl()
	{
		return URL + "login";
	}

	public static string getNewGameUrl()
	{
		return URL + "games/new";
	}

	public static string getRegisterUrl()
	{
		return URL + "user/new";
	}

	public static string postballs()
	{
		return "http://ec2-52-15-203-194.us-east-2.compute.amazonaws.com/v1/user/ball";
	}
	
	// GET METHODS
	public static UnityWebRequest getLoginRequest(string username, string password){
		UnityWebRequest request = UnityWebRequest.Get(getLoginUrl());
        	request.SetRequestHeader("Content-Type", "application/json");
			request.SetRequestHeader("x-api-key","a6b1e5f42b6e494695561692d9c30b6c");
			request.SetRequestHeader("Username", username);
			request.SetRequestHeader("Password", password);
		return request;
	}

	public static UnityWebRequest postNewUser(string parameters) {
		UnityWebRequest request = UnityWebRequest.Put(getRegisterUrl(), parameters);
			request.SetRequestHeader("Content-Type", "application/json");
		return request;
	}

	public static UnityWebRequest postNewGame(string parameters) {
		UnityWebRequest request = UnityWebRequest.Put(getNewGameUrl(), parameters);
			request.SetRequestHeader("Content-Type", "application/json");
		return request;
	}

	public static UnityWebRequest getGamesRequest()
	{
		User user = PersistentData.getUserPrefs();
		UnityWebRequest request = UnityWebRequest.Get("http://ec2-52-15-203-194.us-east-2.compute.amazonaws.com/v1/games/1");
        	request.SetRequestHeader("Content-Type", "application/json");
			request.SetRequestHeader("Username", "laurichu");
			request.SetRequestHeader("Password", "pikachu");
		return request;
	}

	public static UnityWebRequest getAllGamesRequest()
	{
		User user = PersistentData.getUserPrefs();
		UnityWebRequest request = UnityWebRequest.Get("http://ec2-52-15-203-194.us-east-2.compute.amazonaws.com/v1/games/");
        	request.SetRequestHeader("Content-Type", "application/json");
		return request;
	}

	public static UnityWebRequest getBallsRequest(string idGame)
	{
		User user = PersistentData.getUserPrefs();
		UnityWebRequest request = UnityWebRequest.Get("http://ec2-52-15-203-194.us-east-2.compute.amazonaws.com/v1/balls/1");
        	request.SetRequestHeader("Content-Type", "application/json");
			request.SetRequestHeader("Username", "laurichu");
			request.SetRequestHeader("Password", "pikachu");
		return request;
	}

	// POST Request	

	public static UnityWebRequest postBallCatch(string parameters)
	{
		UnityWebRequest request = UnityWebRequest.Put(postballs(),parameters);
		request.SetRequestHeader("Content-Type", "application/json");
		request.SetRequestHeader("Username", "laurichu");
		request.SetRequestHeader("Password", "pikachu");
		return request;
	}
}
