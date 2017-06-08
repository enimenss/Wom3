using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBuffs : NetworkBehaviour
{

    public bool buffedHp;
    public bool unlimitedMana;
    public bool doubleRegen;
   
    public float buffDuration;
    public float manaTmp;


    void Start()
    {
      
        buffDuration = 10f;
    }


    void LateUpdate()
    {
 
        if (unlimitedMana)
        {
            GetComponent<Combat>().mana = manaTmp;
        }
  
    }
    public void buffedHpStart()
    {
      
        this.GetComponent<Combat>().health *= 2;
        this.GetComponent<Combat>().Maxhealth *= 2;
        buffedHp = true;
        Invoke("buffedHpEnd", 10);
      
    }
    public void buffedHpEnd()
    {
        this.GetComponent<Combat>().health /= 2;
        this.GetComponent<Combat>().Maxhealth /= 2;
        buffedHp = false;
    }

    public void unlimitedManaStart()
    {
       
        manaTmp = GetComponent<Combat>().mana;
        unlimitedMana = true;
        Invoke("unlimitedManaEnd", 5);
    }
    public void unlimitedManaEnd()
    {
        unlimitedMana = false;
    }

    public void doubleRegenStart()
    {
      
        this.GetComponent<Combat>().healthRegen *= 2;
        this.GetComponent<Combat>().manaRegen *= 2;
        doubleRegen = true;
        Invoke("doubleRegenEnd", 12);
    }
    public void doubleRegenEnd()
    {
        doubleRegen = false;
        this.GetComponent<Combat>().healthRegen /= 2;
        this.GetComponent<Combat>().manaRegen /= 2;
    }

}
