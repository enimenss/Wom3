using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explosion : NetworkBehaviour
{
    public float time;

    void Start()
    {
        Destroy(this.gameObject, time);
    }

    void Update()
    {

    }
}
