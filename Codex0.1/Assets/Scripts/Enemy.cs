using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour
{
    public Transform target;
    public float speed;
    public float attack1Range;
    public int attack1Damage;
    public float timeBetweenAttacks;
    public Vector3 tmp;


    void Start()
    {
         Rest();
        tmp = new Vector3(7f, 1.5f, 0);

    }
    void Update()
    {
       
        
    }

    public void MoveToPlayer()
    {

        transform.LookAt(target.position);
       // transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        
       // if (Vector3.Distance(transform.position, target.position) > attack1Range)
        {
            transform.Translate(new Vector3(target.transform.position.x * Time.deltaTime, 0, 0));
          
        }
    }

     public void Rest()
     {
       /* transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        if (Vector3.Distance(tmp,transform.position)>0)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }*/
     }
}