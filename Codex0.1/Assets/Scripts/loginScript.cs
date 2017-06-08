using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Class;
using System;

public class loginScript : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text error;

    private API api;

    public delegate void afterdata();

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        api = GetComponent<API>();
    }
    public void LoginD()
    {
        Debug.Log(GetComponent<PlayerData>().token);
        if (GetComponent<PlayerData>().token != null)
        {
            afterdata s = GetUser;
            api.GetUser(s);
        }
    }
    public void GetUser()
    {
        afterdata s = GetUserStat;
        api.GetUserStat(s);
    }
    public void GetUserStat()
    {
        afterdata s = GetUserItems;
        api.GetUserItems(s);
    }
    public void GetUserItems()
    {
      //  Debug.Log(GetComponent<PlayerData>().token.token);
        SceneManager.LoadScene("MainScene");
    }
    public void Login()
    {
        afterdata s = LoginD;
        GetComponent<API>().Login(username.text, password.text, s);

    }

}
