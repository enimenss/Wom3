using Prototype.NetworkLobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Lobby.Scripts.Lobby
{
    public class hook : LobbyHook
    {

        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);
            Debug.Log(lobbyPlayer);
            Debug.Log(gamePlayer);
            Player n = lobbyPlayer.GetComponent<Player>();
            Combat s = gamePlayer.GetComponent<Combat>();
            Debug.Log(n.hero);
            System.Random p = new System.Random();
            //   s.SetParametars(n.playerName, int.Parse(n.hero), 0, 0, n.team);
            s.PlayerName = n.playerName;
            if (!n.hero.Equals(""))
                s.hero = int.Parse(n.hero);
            else
                s.hero = p.Next(1, 4);
            Debug.Log(s.hero);

        }
    }
}
