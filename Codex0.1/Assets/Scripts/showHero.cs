using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showHero : MonoBehaviour
{

    public Text Health;
    public Text Mana;
    public Text HealthReg;
    public Text ManaReg;

    public ShowSpell lc;
    public ShowSpell rc;
    public ShowSpell q;
    public ShowSpell w;
    public ShowSpell e;
    public ShowSpell r;

    public HeroControler Current = null;

    private void Start()
    {
        Health.text = "";
        HealthReg.text = "";
        Mana.text = "";
        ManaReg.text = "";
        lc.Hide();
        rc.Hide();
        q.Hide();
        w.Hide();
        e.Hide();
        r.Hide();
    }

    public void addItem(HeroControler hero)
    {
        if (Current != null)
            Current.outline.SetActive(false);
     
        Current = hero;
        Current.outline.SetActive(true);
        Assets.Class.Hero n = hero.hero;

        Health.text = "Health:" + Current.healt.ToString();
        HealthReg.text = "HealthReg:" + Current.HeltReg.ToString();
        Mana.text = "Mana:" + Current.mana.ToString();
        ManaReg.text = "ManaReg:" + Current.manareg.ToString();
        lc.change(n.lc);
        rc.change(n.rc);
        q.change(n.q);
        w.change(n.w);
        e.change(n.e);
        r.change(n.r);
    }
}
