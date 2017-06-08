using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HolyExplosionController : NetworkBehaviour
{

    public NetworkInstanceId PlayerNetId;
    public float damage;
    void Start()
    {
      
        Invoke("DestroyMe", 7);
    }

    // Update is called once per frame
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
                x.gameObject.GetComponent<Combat>().hit(damage);
                x.gameObject.GetComponent<Combat>().LastHitNetId = PlayerNetId;
                GameObject tmp = NetworkServer.FindLocalObject(PlayerNetId);
                tmp.GetComponent<Combat>().CmdSetPoints(damage);
            }
        }
    }
    void DestroyMe()
    {
        NetworkServer.Destroy(this.gameObject);
    }
}
