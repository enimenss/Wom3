using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloneMovement : NetworkBehaviour
{

    private Vector3 startPosition;
    private float startTime;
    private float journeyLength;
    private Vector3 endPosition;
    private float speed;
    private float endX;
  
    void Start()
    {
        startPosition = this.transform.position;
        startTime = Time.time;
        endX = Random.Range(-15f, 15f);
        endPosition = new Vector3(this.transform.position.x + endX, this.transform.position.y, 0);
        journeyLength = Vector3.Distance(startPosition, endPosition);
        speed = Random.Range(0.4f, 0.9f);
        if (endX < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

   
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
    }
}
