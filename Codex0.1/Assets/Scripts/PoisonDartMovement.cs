using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PoisonDartMovement : NetworkBehaviour
{
    public NetworkInstanceId PlayerNetId;
    private Vector3 startPosition;
    public float distance;
    public float damage;
    public GameObject _poisonTrail;//
    GameObject trailTmp;//
    public float ang;//
    Vector2 x;

    void Start()
    {
        startPosition = this.transform.position;
        distance = 0;
   
        CmdPoisonTrail(this.transform.position);//
        x = this.gameObject.GetComponent<Rigidbody2D>().velocity;
    }
    [Command]//
    public void CmdPoisonTrail(Vector3 DartPosition)//
    {
        GameObject trailTmp2 = Instantiate(this._poisonTrail);
        trailTmp2.transform.position = DartPosition;

        NetworkServer.Spawn(trailTmp2);
        RpcPoisonTrail(trailTmp2);
    }
    [ClientRpc]
    public void RpcPoisonTrail(GameObject x)
    {
        trailTmp = x;
    }
    void Update()
    {
        Vector3 tmp;
        tmp = this.transform.position - startPosition;
        // distance = this.transform.position.sqrMagnitude - startPosition.sqrMagnitude;
        distance = tmp.sqrMagnitude;
        if (distance > 120)
        {
            Destroy(this.gameObject);
            NetworkServer.Destroy(trailTmp.gameObject);//
        }
        if (trailTmp != null)//
            trailTmp.transform.position = this.transform.position;//
      /*  if (x.x > 0)
            ang = -ang;
        this.transform.rotation = Quaternion.Euler(0, 0, ang - 45);*/
    }

    void OnCollisionEnter2D(Collision2D x)
    {
     
        if (x.gameObject.tag == "Platform")
        {
            NetworkServer.Destroy(trailTmp.gameObject);
            NetworkServer.Destroy(this.gameObject);
        }
        else if (x.gameObject.tag == "GameController" )
        {
            //  if(tmp.GetComponent<Combat>().PlayerTeamId != (x.gameObject.GetComponent<Combat>().PlayerTeamId)
            if (x.gameObject.GetComponent<Combat>().PlayerNetId != PlayerNetId)
            {
                x.gameObject.GetComponent<Combat>().CmdHit(damage,PlayerNetId);
                //Debug.Log(x.gameObject.GetComponent<Combat>().PlayerNetId);
               // x.gameObject.GetComponent<Combat>().LastHitNetId = PlayerNetId;
                GameObject tmp = NetworkServer.FindLocalObject(PlayerNetId);
                tmp.GetComponent<Combat>().CmdSetPoints(damage);

            }
            NetworkServer.Destroy(trailTmp.gameObject);
            NetworkServer.Destroy(this.gameObject);
        }
       else if(x.gameObject.tag=="MagicShield")
        {
            NetworkServer.Destroy(trailTmp.gameObject);
            NetworkServer.Destroy(this.gameObject);  
        }
    }    
}
