using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BubbleSpellScript : NetworkBehaviour
{

    public float bubbleHealth;
    public GameObject player;
    public float RegenHealthTimeLastBubble;
    public float RegenHealthTimeWaitBubble;
    // Use this for initialization
    void Start()
    {
        bubbleHealth = player.GetComponent<Combat>().health;
        RegenHealthTimeLastBubble = player.GetComponent<Combat>().RegenHealthTimeLast;
        RegenHealthTimeWaitBubble = player.GetComponent<Combat>().RegenHealthTimeWait;
        Invoke("EndSpell", 9);
    }

    void LateUpdate()
    {
        if (player != null)
            if (RegenHealthTimeLastBubble + RegenHealthTimeWaitBubble < Time.time)
            {
                bubbleHealth += player.GetComponent<Combat>().healthRegen;
                RegenHealthTimeLastBubble = Time.time;
            }
        if (player != null)
        {
            this.transform.position = player.transform.position;
            player.GetComponent<Combat>().health = bubbleHealth;
        }
    }

    void EndSpell()
    {
        player.GetComponent<Combat>().health = bubbleHealth;
        player.GetComponent<Combat>().manaRegen /= 3;
        NetworkServer.Destroy(this.gameObject);
    }
}
