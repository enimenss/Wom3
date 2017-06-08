using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GranadeMovement : NetworkBehaviour
{

    public GameObject explosion;
    public NetworkInstanceId PlayerNetId;
    public int spellType;
    private Vector3 startPosition;
    private float distance;
    public float damage;

    void Start()
    {
        startPosition = this.transform.position;
        distance = 0;
    }

    void Update()
    {
        distance = (this.transform.position - startPosition).sqrMagnitude;
        if (distance > 60)
        {
            GameObject boom = Instantiate(explosion);
            NetworkServer.Spawn(boom);
            boom.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D x)
    {
        if (x.gameObject.tag == "Platform")
        {
            CmdExplosion();
            Destroy(this.gameObject);
        }

    }

    [Command]
    void CmdExplosion()
    {
        GameObject boom = Instantiate(explosion);
        boom.transform.position = this.transform.position;
        NetworkServer.Spawn(boom);
        if (spellType == 1)
        {
            boom.GetComponent<Poison>().damage = damage;
            boom.GetComponent<Poison>().PlayerNetId = PlayerNetId;
        }
        else
            boom.GetComponent<FrostNova>().PlayerNetId = PlayerNetId;


    }


    void OnBecameInvisible()
    {


    }
}
