using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Game
{
	public string id;

	public string num_players;

    public string name;
    public string radio;

    public string passphrase;

    public string lat;

    public string lng;
	
}

public class Games {
    public List<Game> games;
}