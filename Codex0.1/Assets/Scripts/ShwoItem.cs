using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Class;
using UnityEngine.UI;

public class ShwoItem : MonoBehaviour
{
    public Text Health;

    public Text Mana;
    public Text HealthReg;
    public Text ManaReg;
    public Text Armor;
    public Text Demage;
    public Text Price;

    public Text SDemage;

    public Text SArea;

    public Assets.Class.Items Current = null;

    private void Start()
    {
        Health.text = "";
        HealthReg.text = "";
        Mana.text = "";
        ManaReg.text = "";
        Armor.text = "";
        Demage.text = "";
        Price.text = "";
        SDemage.text = "";
        SArea.text = "";
    }

    public void addItem(Assets.Class.Items n)
    {
        Current = n;
        Health.text = "Health:"+Current.Health.ToString();
        HealthReg.text = "HealthReg:"+Current.HealthReg.ToString();
        Mana.text = "Mana:"+Current.Mana.ToString();
        ManaReg.text = "ManaReg:"+Current.ManaReg.ToString();
        Armor.text = "Armor:"+Current.Armor.ToString();
        Demage.text = "Demage:"+Current.Demage.ToString();
        Price.text = "Price:"+Current.Price.ToString();
        if(Current.Spell!=null)
        {
            SDemage.text = "SDemage:"+Current.Spell.Demage.ToString();
            SArea.text = "SArea:"+Current.Spell.Area.ToString();
        }
    }

}
