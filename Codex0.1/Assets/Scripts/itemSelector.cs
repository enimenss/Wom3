using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemSelector : MonoBehaviour, IPointerClickHandler
{

    public GameObject ItemPanel;
    public ItemControler item = null;
    public ItemsControler main;
    public itemSelector other;
    public Image slika;
    public Text errorMassage;
    public int index;
    public Prototype.NetworkLobby.LobbyPlayerList p;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(ItemPanel);
        //GameObject x = gameObject.transform.Find("Items").gameObject;
        //ItemPanel = x;
        ItemPanel.SetActive(true);
        ItemPanel.GetComponent<EventsFoitems>().sel = this;

    }
    public void selectPisture(ItemControler c)
    {
        Debug.Log(c);
        Debug.Log(ItemPanel.GetComponent<EventsFoitems>().prvi[(index ^ 3) - 1]);
        if (ItemPanel.GetComponent<EventsFoitems>().prvi[(index ^ 3) - 1] != null && c.ime == ItemPanel.GetComponent<EventsFoitems>().prvi[(index ^ 3) - 1].ime)
        {
            errorMassage.gameObject.SetActive(true);
            return;
        }
        item = c;
        ItemPanel.GetComponent<EventsFoitems>().prvi[(index) - 1] = c;
        slika.sprite = Resources.Load<Sprite>("Image/" + item.Item.Image) as Sprite;
        ItemPanel.SetActive(false);
        p.prvi[index - 1] = c;
    }
}
