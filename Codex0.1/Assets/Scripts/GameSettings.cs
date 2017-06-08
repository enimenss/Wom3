using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSettings : NetworkBehaviour {
    //Set hero skin,spells,spell textures,cooldowns
    //  GameObject player;
    public int HeroId;
   

    public void SetSpells()
    {

    }
  
   


    [Command]
    public void CmdSetHero()
    {
        RpcGameSettings();
    }
    [ClientRpc]
    public void RpcGameSettings()
    {
        if (HeroId == 1)
        {
            RuntimeAnimatorController tmp = Resources.Load("MageAnimator") as RuntimeAnimatorController;
            this.GetComponent<Animator>().runtimeAnimatorController = tmp;
          //  this.GetComponent<ActionBar>().spell1Name = "teleport";
          
        }
        else if (HeroId == 2)
        {
            RuntimeAnimatorController tmp = Resources.Load("WarlockAnimator") as RuntimeAnimatorController;
            this.GetComponent<Animator>().runtimeAnimatorController = tmp;
           
        }
        else if (HeroId == 3)
        {
            RuntimeAnimatorController tmp = Resources.Load("FiremanAnimator") as RuntimeAnimatorController;
            this.GetComponent<Animator>().runtimeAnimatorController = tmp;
        }
        else if (HeroId == 4)
        {
            RuntimeAnimatorController tmp = Resources.Load("ClericAnimator") as RuntimeAnimatorController;
            this.GetComponent<Animator>().runtimeAnimatorController = tmp;
           
        }

    }


}
