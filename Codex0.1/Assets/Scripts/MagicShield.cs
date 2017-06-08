using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MagicShield : NetworkBehaviour
{

    void Start()
    {

    }

   
    void Update()
    {
      
    }
    [Command]
    public void CmdDestroyMe()
    {
        Destroy(this.gameObject);
    }


  
}
