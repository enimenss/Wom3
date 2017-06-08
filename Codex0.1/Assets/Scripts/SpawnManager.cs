using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnManager : NetworkBehaviour
{

    public GameObject worldBuffCreatorObject;
    public GameObject worldParticlesCreatorObject;

    public override void OnStartServer()
    {
       
        GameObject tmp = Instantiate(worldBuffCreatorObject);
        NetworkServer.Spawn(tmp);

        GameObject tmp1 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp1);
        tmp1.transform.position = new Vector3(0.44f, 1.89f);
        GameObject tmp2 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp2);
        tmp2.transform.position = new Vector3(6.15f, 3.43f);
        GameObject tmp3 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp3);
        tmp3.transform.position = new Vector3(-4.48f, 3.36f);

        GameObject tmp4 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp4);
        tmp4.transform.position = new Vector3(25f, 3.43f);
        GameObject tmp5 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp5);
        tmp5.transform.position = new Vector3(13.15f, 3.43f);
        GameObject tmp6 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp6);
        tmp6.transform.position = new Vector3(19f, 5f);
        tmp5.transform.position = new Vector3(13.15f, 3.43f);
        GameObject tmp10 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp10);
        tmp10.transform.position = new Vector3(18f, 0f);

        GameObject tmp7 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp7);
        tmp7.transform.position = new Vector3(-18f, 3.36f);
        GameObject tmp8 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp8);
        tmp8.transform.position = new Vector3(-14f, 0);
        GameObject tmp9 = Instantiate(worldParticlesCreatorObject);
        NetworkServer.Spawn(tmp9);
        tmp9.transform.position = new Vector3(-12f, 5f);


    }

   
    void Update()
    {

    }
}
