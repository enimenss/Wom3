using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsFoitems : MonoBehaviour
{

    public ItemsControler controler;
    public itemSelector sel;
    public ItemControler[] prvi = new ItemControler[2];

    public void select()
    {
        if(controler.selectedItem!=null)
        {
            sel.selectPisture(controler.selectedItem);
        }
    }
}
