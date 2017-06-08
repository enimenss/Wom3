using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Class;
using UnityEngine.SceneManagement;
using System;

public class API : MonoBehaviour
{

    public string APIAddress = "http://localhost:5611/api/Users1";

    public void Login(string username, string password, Delegate s)
    {
        try
        {
            logUser novi = new logUser { userName = username, hashPassword = password };
            Debug.Log(novi.userName + " " + novi.hashPassword);
            string ourPostData = JsonUtility.ToJson(novi);
            Debug.Log(ourPostData);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            byte[] pData = System.Text.Encoding.ASCII.GetBytes(ourPostData.ToCharArray());
            WWW api = new WWW(APIAddress + @"/Login", pData, headers);
            StartCoroutine(WaitForWWW(api, s));
        }
        catch (UnityException ex) { Debug.Log(ex.Message); }

    }

    IEnumerator WaitForWWW(WWW www, Delegate s1)
    {
        yield return www;


        string txt = "";
        if (string.IsNullOrEmpty(www.error)) //text of success
        {
            Token s = JsonUtility.FromJson<Token>(www.text);
            GetComponent<PlayerData>().token = s;
            Debug.Log(s.token + " " + s.Username);
            txt = www.text;
            GetComponent<loginScript>().error.gameObject.SetActive(false);
        }
        else
        {
            txt = www.error;
            GetComponent<loginScript>().error.gameObject.SetActive(true);
        }
        Debug.Log(txt);
        s1.DynamicInvoke();
    }
    public void Logout()
    {
        try
        {
            Token n = GetComponent<PlayerData>().token;
            if (n == null)
                return;
            WWW api = new WWW(APIAddress + @"/Logout/" + n.token);
            Debug.Log(APIAddress + @"/Logout/" + n.token);
            StartCoroutine(logoutwww(api));
        }
        catch (UnityException ex) { Debug.Log(ex.Message); }

    }

    IEnumerator logoutwww(WWW www)
    {
        yield return www;


        string txt = "";
        if (string.IsNullOrEmpty(www.error)) //text of success
        {
            txt = www.text;
            Destroy(gameObject, 0.2f);

            SceneManager.LoadScene("LOginScene");
        }
        else
            txt = www.error;  //error
        Debug.Log(txt);
    }
    public void GetUser(Delegate s)
    {
        try
        {
            Token n = GetComponent<PlayerData>().token;
            if (n == null)
                return;
            Debug.Log(APIAddress + @"/GetUser/" + n.token);
            WWW api = new WWW(APIAddress + @"/GetUser/" + n.token);
            StartCoroutine(Userwww(api, s));
        }
        catch (UnityException ex) { Debug.Log(ex.Message); }

    }

    IEnumerator Userwww(WWW www, Delegate s1)
    {
        yield return www;


        string txt = "";
        if (string.IsNullOrEmpty(www.error)) //text of success
        {
            txt = www.text;
            User s = JsonUtility.FromJson<User>(www.text);
            GetComponent<PlayerData>().LoginUser = s;
            Debug.Log(s);
        }
        else
            txt = www.error;  //error
        s1.DynamicInvoke();
        Debug.Log(txt);
    }
    public void GetUserStat(Delegate s)
    {
        try
        {
            Token n = GetComponent<PlayerData>().token;
            if (n == null)
                return;
            Debug.Log(APIAddress + @"/GetUserStat/" + n.token);
            WWW api = new WWW(APIAddress + @"/GetUserStat/" + n.token);
            StartCoroutine(UserStatwww(api, s));

        }
        catch (UnityException ex) { Debug.Log(ex.Message); }

    }

    IEnumerator UserStatwww(WWW www, Delegate s1)
    {
        yield return www;


        string txt = "";
        if (string.IsNullOrEmpty(www.error)) //text of success
        {
            txt = www.text;
            UserStats s = JsonUtility.FromJson<UserStats>(www.text);
            GetComponent<PlayerData>().Stats = s;
            Debug.Log(s);
        }
        else
            txt = www.error;  //error
        if(s1!=null)
            s1.DynamicInvoke();
        Debug.Log(txt);
    }
    public void GetUserItems(Delegate s)
    {
        try
        {
            Token n = GetComponent<PlayerData>().token;
            if (n == null)
                return;
            Debug.Log(APIAddress + @"/GetUserItems/" + n.token);
            WWW api = new WWW(APIAddress + @"/GetUserItems/" + n.token);
            StartCoroutine(GetUserItemswww(api, s));
        }
        catch (UnityException ex) { Debug.Log(ex.Message); }

    }

    IEnumerator GetUserItemswww(WWW www, Delegate s1)
    {
        yield return www;


        string txt = "";
        if (string.IsNullOrEmpty(www.error)) //text of success
        {
            txt = www.text;
            Assets.Class.ListOfItems s = JsonUtility.FromJson<Assets.Class.ListOfItems>(www.text);
            GetComponent<PlayerData>().ListofItems = new List<Assets.Class.Items>(s.list);
            Debug.Log(s);
            Debug.Log(GetComponent<PlayerData>().ListofItems.Count);
        }
        else
            txt = www.error;  //error
        if(s1!=null)
            s1.DynamicInvoke();
        Debug.Log(txt);
    }
    public void GameRez(GameResult res)
    {
        try
        {
            Debug.Log(res.Token);
            string ourPostData = JsonUtility.ToJson(res);
            Debug.Log(ourPostData);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            byte[] pData = System.Text.Encoding.ASCII.GetBytes(ourPostData.ToCharArray());
            WWW api = new WWW(APIAddress + @"/SetResult", pData, headers);
            Debug.Log(APIAddress + @"/SetResult");
            StartCoroutine(WaitForWWWstat(api));
        }
        catch (UnityException ex) { Debug.Log(ex.Message); }

    }

    IEnumerator WaitForWWWstat(WWW www)
    {
        yield return www;


        string txt = "";
        if (string.IsNullOrEmpty(www.error)) //text of success
        {

            txt = www.text;
            GetUserStat(null);
            GetUserItems(null);
        }
        else
        {
            txt = www.error;

        }
        Debug.Log(txt);
    }
}
