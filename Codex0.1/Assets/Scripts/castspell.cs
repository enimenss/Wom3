using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class castspell : NetworkBehaviour
{
    public GameObject lisen;
    public GameObject left;
    public GameObject right;
    public float speedLeft;
    public float speedRight;
    public float xOfset;
    public float yOfset;

    private float cooldown;

    private float coolTime;

    // Use this for initialization
    void Start()
    {
        lisen.transform.position = new Vector3(this.transform.position.x + xOfset, this.transform.position.y + yOfset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = mousePosition.x - this.transform.position.x;
            if (x >= 0)
            {
                if (xOfset < 0)
                    xOfset = -xOfset;
            }
            else
            {
                if (xOfset > 0)
                    xOfset = -xOfset;
            }

            lisen.transform.position = new Vector3(this.transform.position.x + xOfset, this.transform.position.y + yOfset, 0);
            if (Input.GetMouseButtonDown(0))
            {
                // if (Time.time > coolTime + cooldown)
                // {
                CmdLeftSpell(mousePosition, lisen.transform.position);
                // coolTime = Time.time;
                //  }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Time.time > coolTime + cooldown)
                {

                    CmdRightSpell(mousePosition, lisen.transform.position);
                    coolTime = Time.time;
                }
            }
        }
    }
    [Command]
    void CmdRightSpell(Vector2 mousePosition, Vector2 player)
    {

        Vector2 velocity = mousePosition - player;
        velocity.Normalize();
        GameObject f = Instantiate(right);


        f.transform.position = new Vector3(player.x, player.y, 0);

        Rigidbody2D n = f.GetComponent("Rigidbody2D") as Rigidbody2D;
        n.velocity = velocity * speedRight;

        float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
        f.transform.Rotate(new Vector3(0, 0, ang));
        NetworkServer.Spawn(f);
        GetComponent<Combat>().CmdManaUse(15);
    }
    [Command]
    void CmdLeftSpell(Vector2 mousePosition, Vector2 player)
    {
        Vector2 velocity = mousePosition - player;

        velocity.Normalize();
        GameObject f = Instantiate(left);

        float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
        float ang2 = ang;
        f.transform.Rotate(new Vector3(0, 0, ang));

        f.transform.position = new Vector3(player.x, player.y, 0);


        Rigidbody2D n = f.GetComponent("Rigidbody2D") as Rigidbody2D;
        n.velocity = velocity * speedLeft;
        GetComponent<Combat>().CmdManaUse(8);
        NetworkServer.Spawn(f);
    }
}
