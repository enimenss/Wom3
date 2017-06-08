using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Poison : NetworkBehaviour
{
    public NetworkInstanceId PlayerNetId;
    public float damage;
    void Start()
    {
       
    }

    void Update()
    {

    }

    void OnParticleCollision(GameObject x)
    {
        if (x.gameObject.tag == "GameController")
        {
            //  if(tmp.GetComponent<Combat>().PlayerTeamId != (x.gameObject.GetComponent<Combat>().PlayerTeamId)
            if (x.gameObject.GetComponent<Combat>().PlayerNetId != PlayerNetId)
            {
                GameObject tmp = NetworkServer.FindLocalObject(PlayerNetId);
             
                x.gameObject.GetComponent<Combat>().hit(damage);

                x.gameObject.GetComponent<Combat>().LastHitNetId = PlayerNetId;
                
                tmp.GetComponent<Combat>().CmdSetPoints(damage);
            }
        }
    }
}
