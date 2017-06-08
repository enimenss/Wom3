using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FrostNova : NetworkBehaviour
{
    public NetworkInstanceId PlayerNetId;
    private GameObject p;
    public float damage;
    void Start()
    {
        damage = 1;
        Invoke("DestroyMe", 4f);
    }

    void Update()
    {

    }
    public void DestroyMe()
    {
        NetworkServer.Destroy(this.gameObject);
    }
    void OnParticleCollision(GameObject x)
    {
        if (x.gameObject.tag == "GameController")
        {
            if (x.gameObject.GetComponent<Combat>().PlayerNetId != PlayerNetId)
            {
                x.gameObject.GetComponent<Movement>().speed = 0f;
                x.gameObject.GetComponent<Movement>().jumpPower = 0f;
                p = x;
                p.GetComponent<Spells>().Invoke("FrostNovaEnd", 5);
            }
        }
    }
   
}
