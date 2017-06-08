using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Prototype.NetworkLobby
{
    public class CustomGame : MonoBehaviour
    {

        public LobbyManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField Players;
        public InputField matchNameInput;

        public RectTransform serverListRect;
        public GameObject serverEntryPrefab;
        public GameObject noServerFound;

        protected int currentPage = 0;
        protected int previousPage = 0;

        static Color OddServerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        static Color EvenServerColor = new Color(.94f, .94f, .94f, 1.0f);

        public void OnEnable()
        {
            ListServers();
        }


        public void ListServers()
        {
            lobbyManager.StartMatchMaker();
            currentPage = 0;
            previousPage = 0;

            foreach (Transform t in serverListRect)
                Destroy(t.gameObject);

            noServerFound.SetActive(false);

            RequestPage(0);
        }

        public void OnGUIMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
        {
            if (matches.Count == 0)
            {
                if (currentPage == 0)
                {
                    noServerFound.SetActive(true);
                }

                currentPage = previousPage;

                return;
            }

            noServerFound.SetActive(false);
            foreach (Transform t in serverListRect)
                Destroy(t.gameObject);

            for (int i = 0; i < matches.Count; ++i)
            {
                GameObject o = Instantiate(serverEntryPrefab) as GameObject;
                Debug.Log(o);
                Debug.Log(matches[i]);
                Debug.Log(lobbyManager);
                o.GetComponent<LobbyServerEntry>().Populate(matches[i], lobbyManager, (i % 2 == 0) ? OddServerColor : EvenServerColor, this);

                o.transform.SetParent(serverListRect, false);
            }
        }

        public void ChangePage(int dir)
        {
            int newPage = Mathf.Max(0, currentPage + dir);

            //if we have no server currently displayed, need we need to refresh page0 first instead of trying to fetch any other page
            if (noServerFound.activeSelf)
                newPage = 0;

            RequestPage(newPage);
        }

        public void RequestPage(int page)
        {
            previousPage = currentPage;
            currentPage = page;
            lobbyManager.matchMaker.ListMatches(page, 6, "CUSTOM", true, 0, 0, OnGUIMatchList);
        }

        public void createServer()
        {
            Debug.Log(Players.text);
            lobbyManager.matchMaker.CreateMatch(
               "CUSTOM" + matchNameInput.text,
               (uint)int.Parse(Players.text),
               true,
               "", "", "", 0, 0,
               lobbyManager.OnMatchCreate);
            noServerFound.SetActive(false);
        }
        public void turnOffpanel()
        {
            noServerFound.SetActive(false);
        }
    }

}


