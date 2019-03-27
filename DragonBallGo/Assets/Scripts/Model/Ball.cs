using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Ball
{
	public string id;

	public string game;

    public string ball;

    public string catched;

    public string user;

    public string lat;

    public string lng;
	
}

public class Balls {
    public List<Ball> balls;
}