using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireJumpSpellTrail : NetworkBehaviour
{

    public GameObject player;
    // public Vector3 position;
    // Use this for initialization
    void Start()
    {
        // position = this.transform.position;
        Invoke("CmdDestroyMe", 0.38f);
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.position = player.transform.position;
    }
    [Command]
    public void CmdDestroyMe()
    {
        Destroy(this.gameObject);
    }
}
