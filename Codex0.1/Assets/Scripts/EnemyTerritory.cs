using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyTerritory : NetworkBehaviour
{
    public BoxCollider2D territory;
    GameObject player;
    public bool playerInTerritory;
    
    public GameObject enemy;
    Enemy mob;

  
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("GameController");
        mob = enemy.GetComponent<Enemy>();
        playerInTerritory = false;
    }

   
    void Update()
    {
        if (playerInTerritory == true)
        {
            mob.MoveToPlayer();
        }
        else
        {
            mob.Rest();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GameController")
        {
            playerInTerritory = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "GameController")
        {
            playerInTerritory = false;
        }
    }
}