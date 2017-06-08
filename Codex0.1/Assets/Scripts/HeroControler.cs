using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Class;





public class HeroControler : MonoBehaviour ,IPointerClickHandler
{
   
    public showHero parent;
    public GameObject outline;
    public Text name;
    public Hero hero;
    public string Name;
    public float healt;
    public float mana;
    public float HeltReg;
    public float manareg;

    public string lc;
    public string rc;
    public string q;
    public string w;
    public string e;
    public string r;
    public void start()
    {
        Debug.Log("heros");
        outline.SetActive(false);
        hero = new Hero { Name = Name, healt = healt, mana = mana, HeltReg = HeltReg, manareg = manareg, lc = lc, rc = rc, q = q, w = w, e = e, r = r };
    
        //hero.Name = Name;
        name.text = hero.Name;
        //hero.healt = healt;
        //hero.mana = mana;
        //hero.HeltReg = HeltReg;
        //hero.manareg = manareg;
        //hero.lc = lc;
        //hero.rc = rc;
        //hero.q = q;
        //hero.w = w;
        //hero.e = e;
        //hero.r = r;
        Debug.Log(hero);
    }
    public void OnEnable()
    {
        Debug.Log("heros");
        outline.SetActive(false);
        hero = new Hero { Name = Name, healt = healt, mana = mana, HeltReg = HeltReg, manareg = manareg, lc = lc, rc = rc, q = q, w = w, e = e, r = r };

        //hero.Name = Name;
        name.text = hero.Name;
        //hero.healt = healt;
        //hero.mana = mana;
        //hero.HeltReg = HeltReg;
        //hero.manareg = manareg;
        //hero.lc = lc;
        //hero.rc = rc;
        //hero.q = q;
        //hero.w = w;
        //hero.e = e;
        //hero.r = r;
        Debug.Log(hero);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnMouseDown");
        parent.addItem(this);
    }
}
