using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
    //List of players in the lobby
    public class LobbyPlayerList : MonoBehaviour
    {
        public static LobbyPlayerList _instance = null;

        public Player localPlayer;

        public RectTransform Freands;
        public RectTransform Enemy;
        public GameObject warningDirectPlayServer;
        public Transform Redy;
        public Button swc;

        protected VerticalLayoutGroup _layout1;
        protected VerticalLayoutGroup _layout2;
        protected List<Player> _frends = new List<Player>();
        protected List<Player> _enemy = new List<Player>();
        public HerosSel Selected2=null;
        public ItemControler[] prvi = new ItemControler[2];
        public void OnEnable()
        {
            _instance = this;
            _layout1 = Freands.GetComponent<VerticalLayoutGroup>();
            _layout2 = Enemy.GetComponent<VerticalLayoutGroup>();
            if (GameObject.Find("controler").GetComponent<PlayerData>().Type != GameType.Custom)
            {
                swc.gameObject.SetActive(false);
            }
        }

        public void DisplayDirectServerWarning(bool enabled)
        {
            if (warningDirectPlayServer != null)
                warningDirectPlayServer.SetActive(enabled);
        }

        void Update()
        {
            //this dirty the layout to force it to recompute evryframe (a sync problem between client/server
            //sometime to child being assigned before layout was enabled/init, leading to broken layouting)

            //if(_layout1)
            //    _layout1.childAlignment = Time.frameCount%2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
            //if (_layout2)
            //    _layout2.childAlignment = Time.frameCount % 2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
        }

        public void AddPlayer(Player player)
        {
            if (_frends.Contains(player) || _enemy.Contains(player))
                return;


            int n = _frends.Count;
            int m = _enemy.Count;
            if (n == 5)
            {
                _enemy.Add(player);
                player.gameObject.transform.SetParent(Enemy, false);
                player.parent = "enemy";
                player.team = 2;
            }
            else
            {
                if (m == 5)
                {
                    _frends.Add(player);
                    player.gameObject.transform.SetParent(Freands, false);
                    player.parent = "friends";
                    player.team = 1;
                }
                else
                {
                    if (n > m)
                    {
                        _enemy.Add(player);
                        player.gameObject.transform.SetParent(Enemy, false);
                        player.parent = "enemy";
                        player.team = 2;
                    }
                    else
                    {
                        _frends.Add(player);
                        player.gameObject.transform.SetParent(Freands, false);
                        player.parent = "friands";
                        player.team = 1;
                    }
                }
            }

            player.gameObject.SetActive(true);
            PlayerListModified();
        }

        public void RemovePlayer(Player player)
        {
            if (_frends.Find(x => x == player) != null)
                _frends.Remove(player);
            else
                _enemy.Remove(player);
            PlayerListModified();
        }

        public void PlayerListModified()
        {
            int i = 0;
            foreach (Player p in _frends)
            {
                p.OnPlayerListChanged(i);
                ++i;
                p.CmdchangeHeor(p.hero);
            }
            foreach (Player p in _enemy)
            {
                p.OnPlayerListChanged(i);
                ++i;
                p.CmdchangeHeor(p.hero);
            }
        }
        public void changeParent(string str, GameObject p)
        {
            Debug.Log(str);
            if (str.Equals("enemy"))
            {
                p.transform.SetParent(Enemy, false);
            }
            else
            {
                p.transform.SetParent(Freands, false);
            }
        }
        public void switchPlace()
        {
            Debug.Log(localPlayer);
            Player p = _frends.Find(x => x.localPlayer1 == true);
            for (int i = 0; i < _frends.Count; i++)
                Debug.Log(_frends[i].hero);
            Debug.Log("enemy");
            for (int i = 0; i < _enemy.Count; i++)
                Debug.Log(_enemy[i]);
            Debug.Log(p);
            if (p != null)
            {
                _frends.Remove(p);
                _enemy.Add(p);
                p.gameObject.transform.SetParent(Enemy, false);
                p.parent = "enemy";
                p.team = 2;
            }
            else
            {
                p = _enemy.Find(x => x.localPlayer1 == true);
                _enemy.Remove(p);
                _frends.Add(p);
                p.gameObject.transform.SetParent(Freands, false);
                p.parent = "firends";
                p.team = 1;
            }
        }
        public void LockPlayer()
        {
            Debug.Log(localPlayer);
            Player p = _frends.Find(x => x.localPlayer1 == true);
            for (int i = 0; i < _frends.Count; i++)
                Debug.Log(_frends[i].localPlayer1);
            Debug.Log("enemy");
            for (int i = 0; i < _enemy.Count; i++)
                Debug.Log(_enemy[i]);
            Debug.Log(p);
            Debug.Log(Selected2);
            if (p != null)
            {

                p.CmdchangeHeor(Selected2.slika);
                p.prvi[0] = prvi[0];
                p.prvi[1] = prvi[1];
                //p.HERO.sprite = Resources.Load<Sprite>("Image/" + p.hero) as Sprite;
            }
            else
            {
                p = _enemy.Find(x => x.localPlayer1 == true);
                Debug.Log(p);
                p.CmdchangeHeor(Selected2.slika);
                p.prvi[0] = prvi[0];
                p.prvi[1] = prvi[1];

            }
        }
        public void Select(HerosSel novi)
        {
            if(Selected2!=null)
                Selected2.outline.SetActive(false);
            Selected2 = novi;
            Selected2.outline.SetActive(true);
        }
    }
}
