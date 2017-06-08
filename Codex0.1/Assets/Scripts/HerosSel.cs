using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HerosSel : MonoBehaviour, IPointerClickHandler
{

    public Prototype.NetworkLobby.LobbyPlayerList p;
    public string slika;
    public void OnPointerClick(PointerEventData eventData)
    {
        p.Selected2 = slika;
        Debug.Log(slika);
    }
}
