using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CameraMovement : NetworkBehaviour
{
   
    public float moveSpeed;
    void Start()
    {
        moveSpeed = 0.75f;
        if (isLocalPlayer)
            Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -15);
       
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
           // Vector3 offset = this.transform.position - Camera.main.transform.position;

            var d = (Input.GetAxis("Mouse ScrollWheel"));
            
            Camera.main.orthographicSize -= d;
            if (Camera.main.orthographicSize < 0.2f)
                Camera.main.orthographicSize = 0.2f;
            if (Camera.main.orthographicSize > 3f)
                Camera.main.orthographicSize = 3f;

            //  Camera.main.transform.position += new Vector3(offset.x, offset.y, 0);
            Camera.main.transform.position = Vector3.Lerp(this.transform.position, Camera.main.transform.position, moveSpeed)+new Vector3(0,0,-15);
        }
    }
}
