using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public GameObject CreatePanel;

    public void onCreateServer()
    {
        Debug.Log("create");
        CreatePanel.SetActive(true);
    }

    public void CloseCreateServer()
    {
        CreatePanel.SetActive(false);
    }

    public void ExitLobby()
    {
      //  match.Match.s_Singleton.ExtiToMain();
    }
    public void StopHost()
    {
       // match.Match.s_Singleton.stop();
    }
}
