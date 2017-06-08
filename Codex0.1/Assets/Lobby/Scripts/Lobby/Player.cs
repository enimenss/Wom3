using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.NetworkLobby
{
    public class Player : NetworkLobbyPlayer
    {
        public bool localPlayer1;
        public Button readyButton;
        public Text name;
        public GameObject localIcone;
        [SyncVar(hook = "OnMyName")]
        public string playerName = "";
        [SyncVar(hook = "OnMyColor")]
        public Color playerColor = Color.white;
        [SyncVar(hook = "OnParent")]
        public string parent;
        [SyncVar(hook = "onCaracter")]
        public string caracter;

        [SyncVar(hook = "onLock")]
        public string hero;
        public Image HERO;
        public Color localPlayer;
        public Color otherPlayer;
        public Color UnRedy;
        public Color redy;
        public int team = 0;
        public Image color;

        public ItemControler[] prvi = new ItemControler[2];

        public override void OnClientEnterLobby()
        {
            base.OnClientEnterLobby();

            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(1);

            LobbyPlayerList._instance.AddPlayer(this);
            LobbyPlayerList._instance.DisplayDirectServerWarning(isServer && LobbyManager.s_Singleton.matchMaker == null);
            // LobbyPlayerList._instance.localPlayer = this;
            if (isLocalPlayer)
            {
                SetupLocalPlayer();
                LobbyPlayerList._instance.localPlayer = this;
            }
            else
            {

                SetupOtherPlayer();
            }

            //setup the player data on UI. The value are SyncVar so the player
            //will be created with the right value currently on server
            OnMyName(playerName);
            // OnMyColor(playerColor);
        }
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            //if we return from a game, color of text can still be the one for "Ready"
            readyButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;

            SetupLocalPlayer();
        }

        void ChangeReadyButtonColor(Color c)
        {
            color.color = c;
        }

        void SetupOtherPlayer()
        {
            Debug.Log("SetupOtherPlayer");
            ChangeReadyButtonColor(UnRedy);
            readyButton.gameObject.SetActive(false);
            name.text = playerName;
            OnClientReady(false);
            localPlayer1 = false;
        }

        void SetupLocalPlayer()
        {
            //remoteIcone.gameObject.SetActive(false);
            //localIcone.gameObject.SetActive(true);
            Debug.Log("SetupLocalPlayer");
            CheckRemoveButton();

            if (playerColor == Color.white)
                CmdColorChange();
            readyButton.gameObject.SetActive(true);
            ChangeReadyButtonColor(localPlayer);
            playerName = GameObject.Find("controler").GetComponent<PlayerData>().LoginUser.Username;
            readyButton.transform.GetChild(0).GetComponent<Text>().text = "JOIN";
            readyButton.interactable = true;
            name.text = playerName;

            //have to use child count of player prefab already setup as "this.slot" is not set yet
            //if (playerName == "")
            //    CmdNameChanged("Player" + (LobbyPlayerList._instance.playerListContentTransform.childCount-1));

            //we switch from simple name display to name input
            GameObject.Find("NoServerFound").SetActive(false);
            readyButton.onClick.RemoveAllListeners();
            readyButton.onClick.AddListener(OnReadyClicked);

            //when OnClientEnterLobby is called, the loval PlayerController is not yet created, so we need to redo that here to disable
            //the add button if we reach maxLocalPlayer. We pass 0, as it was already counted on OnClientEnterLobby
            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(0);
            localPlayer1 = true;


        }

        //This enable/disable the remove button depending on if that is the only local player or not
        public void CheckRemoveButton()
        {
            //if (!isLocalPlayer)
            //    return;

            //int localPlayerCount = 0;
            //foreach (PlayerController p in ClientScene.localPlayers)
            //    localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;

            //removePlayerButton.interactable = localPlayerCount > 1;
        }

        public override void OnClientReady(bool readyState)
        {
            if (readyState)
            {
                ChangeReadyButtonColor(isLocalPlayer ? redy : otherPlayer);

                Text textComponent = readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = "READY";
                textComponent.color = redy;
                readyButton.interactable = false;
            }
            else
            {
                ChangeReadyButtonColor(isLocalPlayer ? localPlayer : UnRedy);

                Text textComponent = readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = isLocalPlayer ? "JOIN" : "...";
                textComponent.color = Color.white;
                readyButton.interactable = isLocalPlayer;
            }
        }

        public void OnPlayerListChanged(int idx)
        {

        }

        ///===== callback from sync var

        public void OnMyName(string newName)
        {
            playerName = newName;
        }
        public void onLock(string newHero)
        {
            hero = newHero;
            Debug.Log("change" + hero);
            HERO.sprite = Resources.Load<Sprite>("Image/" + hero) as Sprite;
        }
        [Command]
        public void CmdchangeHeor(string hero)
        {
            RpcloadImage(hero);
        }
        [ClientRpc]
        public void RpcloadImage(string Hero)
        {
            hero = Hero;
            Debug.Log("change" + hero);
            HERO.sprite = Resources.Load<Sprite>("Image/" + hero) as Sprite;
        }
        public void OnMyColor(Color newColor)
        {
            playerColor = newColor;
            this.GetComponent<Image>().color = newColor;
        }
        public void OnParent(string novo)
        {
            parent = novo;
            LobbyPlayerList._instance.changeParent(parent, this.gameObject);
        }
        public void onCaracter(string str)
        {
            caracter = str;
        }
        //===== UI Handler

        //Note that those handler use Command function, as we need to change the value on the server not locally
        //so that all client get the new value throught syncvar
        public void OnColorClicked()
        {
            CmdColorChange();
        }
        public void OnparentChange(string std)
        {

            Cmdparent(std);
        }
        public void OnReadyClicked()
        {
            SendReadyToBeginMessage();
        }

        public void OnNameChanged(string str)
        {
            CmdNameChanged(str);
        }

        public void OnRemovePlayerClick()
        {
            if (isLocalPlayer)
            {
                RemovePlayer();
            }
            else if (isServer)
                LobbyManager.s_Singleton.KickPlayer(connectionToClient);

        }

        public void ToggleJoinButton(bool enabled)
        {
            readyButton.gameObject.SetActive(enabled);
            // waitingPlayerButton.gameObject.SetActive(!enabled);
        }

        [ClientRpc]
        public void RpcUpdateCountdown(int countdown)
        {
            LobbyManager.s_Singleton.countdownPanel.UIText.text = "Match Starting in " + countdown;
            LobbyManager.s_Singleton.countdownPanel.gameObject.SetActive(countdown != 0);
        }

        [ClientRpc]
        public void RpcUpdateRemoveButton()
        {
            CheckRemoveButton();
        }

        //====== Server Command

        [Command]
        public void CmdColorChange()
        {

        }

        [Command]
        public void CmdNameChanged(string name)
        {
            playerName = name;
        }
        [Command]
        public void Cmdparent(string name)
        {
            parent = name;
        }
        //Cleanup thing when get destroy (which happen when client kick or disconnect)
        public void OnDestroy()
        {
            LobbyPlayerList._instance.RemovePlayer(this);
            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(-1);
        }
    }
}
