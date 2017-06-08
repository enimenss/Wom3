using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldBuff : NetworkBehaviour
{
    public float RespawnTime;
    public float LastCheckTime;

    public GameObject[] ObjectsArray = new GameObject[5];
    public List<Vector3> PositionArray = new List<Vector3>();
    public List<Vector3> PositionQueue = new List<Vector3>();

    System.Random rnd = new System.Random();

    public override void OnStartServer()
    {
        PositionArray.Clear();
        PositionQueue.Clear();
        LastCheckTime = Time.time;
        PositionArray.Add(new Vector3(-4f, 0.3f, 0));
        PositionArray.Add(new Vector3(-2f, 0.3f, 0));
        PositionArray.Add(new Vector3(0f, 0.3f, 0));
        PositionArray.Add(new Vector3(2f, 0.3f, 0));
        PositionArray.Add(new Vector3(4f, 0.3f, 0));
        PositionArray.Add(new Vector3(-4f, 1.9f, 0));
        PositionArray.Add(new Vector3(-3f, 1.9f, 0));
        PositionArray.Add(new Vector3(1f, 3.1f, 0));
        PositionArray.Add(new Vector3(5f, 2.3f, 0));
        PositionArray.Add(new Vector3(6f, 2.3f, 0));
        PositionArray.Add(new Vector3(7f, 2.3f, 0));

        PositionArray.Add(new Vector3(24f, 2.5f, 0));
        PositionArray.Add(new Vector3(28f, 0.3f, 0));
        PositionArray.Add(new Vector3(25f, 0.3f, 0));
        PositionArray.Add(new Vector3(21f, 0.3f, 0));
        PositionArray.Add(new Vector3(15f, 1.3f, 0));
        PositionArray.Add(new Vector3(28, 5.5f, 0));

        PositionArray.Add(new Vector3(25f, 2.5f, 0));
        PositionArray.Add(new Vector3(27f, 0.3f, 0));
        PositionArray.Add(new Vector3(24f, 0.3f, 0));
        PositionArray.Add(new Vector3(22f, 0.3f, 0));
        PositionArray.Add(new Vector3(14f, 1.3f, 0));
        PositionArray.Add(new Vector3(28f, 2.5f, 0));
        PositionArray.Add(new Vector3(30f, 5.5f, 0));
        PositionArray.Add(new Vector3(32f, 5.5f, 0));
        PositionArray.Add(new Vector3(-5f, -0.5f, 0));
        PositionArray.Add(new Vector3(-10f, 0, 0));
        PositionArray.Add(new Vector3(-8f, 0, 0));

        PositionArray.Add(new Vector3(-12f, 3.2f, 0));
        PositionArray.Add(new Vector3(-15f, 3.2f, 0));
        PositionArray.Add(new Vector3(-19f, 6.2f, 0));
        PositionArray.Add(new Vector3(-22f, 3.5f, 0));
        PositionArray.Add(new Vector3(-20f, 2f, 0));
        PositionArray.Add(new Vector3(-10f, 1f, 0));
        PositionArray.Add(new Vector3(-13f, 1f, 0));
        PositionArray.Add(new Vector3(-12f, 1f, 0));
        PositionArray.Add(new Vector3(-16f, 3.2f, 0));
        PositionArray.Add(new Vector3(-13f, 3.2f, 0));
        PositionArray.Add(new Vector3(-21f, 2f, 0));
    }
    void Update()
    {
        if (isServer)
        {
            if (LastCheckTime + RespawnTime < Time.time)
            {
                LastCheckTime = Time.time;
                int tmpObject = rnd.Next(0, 5);
                int tmpPosition = rnd.Next(0, PositionArray.Count);
                CmdRespawnObjects(tmpObject, tmpPosition);
            }
        }
    }
    [Command]
    void CmdRespawnObjects(int rndObject, int rndPosition)
    {
        if (PositionArray.Count != 0)
        {
            GameObject _buff = Instantiate(ObjectsArray[rndObject]);
            _buff.transform.position = PositionArray[rndPosition];
            _buff.transform.rotation = Quaternion.identity;
            PositionQueue.Add(_buff.transform.position);
            PositionArray.Remove(_buff.transform.position);
            NetworkServer.Spawn(_buff);
        }
        else
        {
            for (int i = 0; i < PositionQueue.Count; i++)
            {
                PositionArray.Add(PositionQueue[i]);
            }
            PositionQueue.Clear();
        }
    }
}
