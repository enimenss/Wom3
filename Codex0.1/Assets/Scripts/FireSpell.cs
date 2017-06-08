using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireSpell : NetworkBehaviour
{
    public NetworkInstanceId PlayerNetId;

    public float damage;
    void Start()
    {

    }

    void Update()
    {

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

    [Command]
    public void CmdDestroyMe(float time)
    {
        Destroy(this.gameObject,time);
    }

    void OnParticleCollision(GameObject x)
    {
        if (x.gameObject.tag == "GameController")
        {
            if (x.gameObject.GetComponent<Combat>().PlayerNetId != PlayerNetId)
            {
                x.gameObject.GetComponent<Combat>().hit(damage);
                x.gameObject.GetComponent<Combat>().LastHitNetId = PlayerNetId;
                GameObject tmp = NetworkServer.FindLocalObject(PlayerNetId);
                tmp.GetComponent<Combat>().CmdSetPoints(damage);
            }

        }
    }
}
