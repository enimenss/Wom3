using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Laser : NetworkBehaviour
{
    public float damage;
    public NetworkInstanceId PlayerNetId;

    void Start()
    {
      // NetworkServer.FindLocalObject(PlayerNetId).GetComponent<Laser>().damage = this.GetComponent<Combat>().heroDamage / 20;
        //  Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("AreaEffector").GetComponent<Collider2D>());
    }

    void Update()
    {

    }

    [Command]
    public void CmdDestroyMe(float t)
    {
        Destroy(this.gameObject, t);
    }

    [Command]
    public void CmdStopEmission()
    {
        RpcStopEmission();
    }
    [ClientRpc]
    public void RpcStopEmission()
    {
        GetComponent<ParticleSystem>().enableEmission = false;
    }

    void OnParticleCollision(GameObject x)
    {  
        if (x.gameObject.tag == "GameController")
        {
            //  if(tmp.GetComponent<Combat>().PlayerTeamId != (x.gameObject.GetComponent<Combat>().PlayerTeamId)
         //   if (x.gameObject.GetComponent<Combat>().PlayerNetId != PlayerNetId)
            {
                x.GetComponent<Combat>().hit(damage);
                x.gameObject.GetComponent<Combat>().LastHitNetId = PlayerNetId;
                GameObject tmp = NetworkServer.FindLocalObject(PlayerNetId);
                tmp.GetComponent<Combat>().CmdSetPoints(damage);
            }
        }
    }
   
}
