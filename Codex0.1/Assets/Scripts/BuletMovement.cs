using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuletMovement : NetworkBehaviour
{
    private Vector3 startPosition;
    public float distance;
    public float damage;
    public NetworkInstanceId PlayerNetId;

    // Use this for initialization
    void Start()
    {
        startPosition = this.transform.position;
        distance = 0;
     
        
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
            
        }
       
    }

    void OnCollisionEnter2D(Collision2D x)
    {
        if (x.gameObject.tag == "Platform")
        {
            Destroy(this.gameObject);
           
        }
        else if (x.gameObject.tag == "GameController")
        {
            
            x.gameObject.GetComponent<Combat>().hit(damage);

            x.gameObject.GetComponent<Combat>().LastHitNetId = PlayerNetId;
            GameObject tmp = NetworkServer.FindLocalObject(PlayerNetId);
            tmp.GetComponent<Combat>().CmdSetPoints(damage);

            Destroy(this.gameObject);
        
        }

    }

}
