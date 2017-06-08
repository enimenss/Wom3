using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Class;
using UnityEngine.EventSystems;
using System;

public class ItemControler : MonoBehaviour,IPointerClickHandler
{
    public Assets.Class.Items Item;
    public Image slika;
    public Text ime;
    public ItemsControler parent;
    public GameObject outline;
   public void populate(Assets.Class.Items item,ItemsControler p)
    {
        Item = item;
        parent = p;
        outline.SetActive(false);
        slika.sprite = Resources.Load<Sprite>("Image/" + Item.Image);
        ime.text = Item.Image;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnMouseDown");
        GameObject.Find("ItemPanel").GetComponent<ShwoItem>().addItem(Item);
        parent.selectItem(this);
    }
}
