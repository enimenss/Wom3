using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldParticlesController : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject x)
    {
        if (x.gameObject.tag == "GameController")
        {
            x.GetComponent<Combat>().CmdManaUp(2);
        }
    }
}
