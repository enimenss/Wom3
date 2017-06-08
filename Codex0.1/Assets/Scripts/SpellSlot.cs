using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellSlot:NetworkBehaviour
{
    public string id;
    public Texture2D SpellTexture;
    public string tooltip;
    

    void Start()
    {
        SetSpell();
    }

    public void SetSpell()
    {

    }
  
    void Update()
    {

    }


}
