using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData
{
    static public string ID = "Id";

    static public string USERNAME = "Username";

    static public string PASSWORD = "Password";

    static public string NAME = "Name";

    static public string GAME_ID = "GameId";
    public static void saveUserPrefs(User user){
        PlayerPrefs.SetString(ID, user.id);
        PlayerPrefs.SetString(USERNAME, user.username);
        PlayerPrefs.SetString(PASSWORD, user.password);
        PlayerPrefs.SetString(NAME, user.name);
    }

    public static void saveSelectedGame(string id)
    {
        PlayerPrefs.SetString(GAME_ID, id);
    }

    public static string getSelectedGameId()
    {
        return PlayerPrefs.GetString(GAME_ID);
    }

    public static User getUserPrefs()
    {
        User user = new User();
        user.id = getId();
        user.username = getUsername();
        user.password = getPassword();
        user.name = getName();
        return user;
    }

    public static string getUsername()
    {
        return PlayerPrefs.GetString(USERNAME);
    }
    static string getPassword()
    {
        return PlayerPrefs.GetString(USERNAME);
    }
    static string getId()
    {
        return PlayerPrefs.GetString(USERNAME);
    }
    static string getName()
    {
        return PlayerPrefs.GetString(USERNAME);
    }
}