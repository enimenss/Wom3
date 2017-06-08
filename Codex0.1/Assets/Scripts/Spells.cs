using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spells : NetworkBehaviour
{
    public float damage;
    public GameObject bulet;
    public float buletSpeed;
    public GameObject granadePoison;
    public GameObject granadeFrostNova;
    public float granadeSpeed;
    public GameObject _laser;
    public GameObject _holyLight;
    public GameObject _fireSpell;
    public GameObject holyExplosion;
    public GameObject _bubble;
    public GameObject _fireJumpTrail;

    public GameObject clone;
    GameObject tmpClone1;
    GameObject tmpClone2;
    GameObject tmpClone3;
    GameObject tmpClone4;
    GameObject tmpClone5;


    public GameObject MageShield;
    public GameObject WarlockShield;
    public GameObject ClericShield;
    public GameObject FiremanShield;

    public GameObject _poisonDart;
    private int _transformSkin;
    public GameObject portalPrefab;
    public GameObject portalObject;
    public bool portalCreated;

    void Start()
    {
        portalCreated = false;
    }

    void Update()
    {

    }
    [Command]
    public void CmdHolyExplosionStart(Vector3 mousePoistion)
    {
        GameObject tmp = Instantiate(holyExplosion);
        NetworkServer.Spawn(tmp);
        tmp.transform.position = mousePoistion;
        RpcHolyExplosion(tmp);
    }
    [ClientRpc]
    public void RpcHolyExplosion(GameObject x)
    {
        x.GetComponent<HolyExplosionController>().damage = this.GetComponent<Combat>().heroDamage / 20;
        x.GetComponent<HolyExplosionController>().PlayerNetId = this.netId;
    }

    [Command]
    public void CmdMakeClones()
    {
        RpcMakeClones();
        if (tmpClone1 != null)
        {
            NetworkServer.Spawn(tmpClone1);
            NetworkServer.Spawn(tmpClone2);
            NetworkServer.Spawn(tmpClone3);
            NetworkServer.Spawn(tmpClone4);
            NetworkServer.Spawn(tmpClone5);
        }
    }

    [ClientRpc]
    public void RpcMakeClones()
    {
        tmpClone1 = Instantiate(clone);
        tmpClone2 = Instantiate(clone);
        tmpClone3 = Instantiate(clone);
        tmpClone4 = Instantiate(clone);
        tmpClone5 = Instantiate(clone);


        tmpClone1.transform.position = new Vector3(this.transform.position.x + Random.Range(0.2f, 2), this.transform.position.y, 0);
        tmpClone2.transform.position = new Vector3(this.transform.position.x - Random.Range(0.2f, 2), this.transform.position.y, 0);
        tmpClone3.transform.position = this.transform.position;
        tmpClone4.transform.position = new Vector3(this.transform.position.x + Random.Range(1.5f, 3.2f), this.transform.position.y, 0);
        tmpClone5.transform.position = new Vector3(this.transform.position.x - Random.Range(1.3f, 3.1f), this.transform.position.y, 0);
        GetComponent<Combat>().isVisible = false;
    }
    [Command]
    public void CmdCloneEnds()
    {
        RpcCloneEnds();
    }
    [ClientRpc]
    public void RpcCloneEnds()
    {
        GetComponent<Combat>().isVisible = true;
        NetworkServer.Destroy(tmpClone1);
        NetworkServer.Destroy(tmpClone2);
        NetworkServer.Destroy(tmpClone3);
        NetworkServer.Destroy(tmpClone4);
        NetworkServer.Destroy(tmpClone5);
    }
    [Command]
    public void CmdGetSmall()
    {
        RpcGetSmall();
    }
    [ClientRpc]
    public void RpcGetSmall()
    {
        GetComponent<Transform>().localScale += new Vector3(-0.7f, -0.7f, -0.7f);
        GetComponent<Movement>().speed = 0.7f;
        GetComponent<Combat>().heroDamage = 30;
    }
    [Command]
    public void CmdGetBig()
    {
        RpcGetBig();
    }
    [ClientRpc]
    public void RpcGetBig()
    {
        GetComponent<Transform>().localScale += new Vector3(0.7f, 0.7f, 0.7f);
        GetComponent<Movement>().speed = 0.5f;
        GetComponent<Combat>().heroDamage = 20;
    }
    [Command]
    public void CmdCreatePortal()
    {
        RpcCreatePortal();
        if (portalObject != null)
            NetworkServer.Spawn(portalObject);
    }
    [ClientRpc]
    public void RpcCreatePortal()
    {
        portalCreated = true;
        portalObject = Instantiate(portalPrefab);

        portalObject.transform.position = this.transform.position;
    }
    [Command]
    public void CmdPortalTeleport()
    {
        RpcPortalTeleport();
    }
    [ClientRpc]
    public void RpcPortalTeleport()
    {

        portalCreated = false;
        this.transform.position = portalObject.transform.position;
        NetworkServer.Destroy(portalObject);
    }

    [Command]
    public void CmdLaser(Vector3 position, float ang, float tmp)
    {
        GameObject newParticleSystem = Instantiate(_laser);
        newParticleSystem.transform.position = position;
        newParticleSystem.transform.rotation = Quaternion.Euler(ang, tmp * 90, 0);
        NetworkServer.SpawnWithClientAuthority(newParticleSystem, base.connectionToClient);
        RpcSetLaser(newParticleSystem);
    }
    [ClientRpc]
    public void RpcSetLaser(GameObject x)
    {
        x.GetComponent<Laser>().PlayerNetId = this.netId;
        x.GetComponent<Laser>().damage = this.GetComponent<Combat>().heroDamage / 20;
        if (isLocalPlayer)
        {
            GetComponent<ActionBar>().tmpSpell2 = x;
        }
    }



    [Command]
    public void CmdHolyLight(Vector3 position, float ang, float tmp)
    {
        GameObject newParticleSystem = Instantiate(_holyLight);
        newParticleSystem.transform.position = position;
        newParticleSystem.transform.rotation = Quaternion.Euler(ang, tmp * 90, 0);
        NetworkServer.SpawnWithClientAuthority(newParticleSystem, base.connectionToClient);
        RpcSetLaser(newParticleSystem);
    }
    [ClientRpc]
    public void RpcSetHolyLight(GameObject x)
    {
        x.GetComponent<Laser>().PlayerNetId = this.netId;
        x.GetComponent<Laser>().damage = this.GetComponent<Combat>().heroDamage / 20;
        if (isLocalPlayer)
        {
            GetComponent<ActionBar>().tmpSpell2 = x;
        }
    }
    [Command]
    public void CmdFireParticleSpell(Vector3 position, float ang, float tmp)
    {
        GameObject newParticleSystem = Instantiate(_fireSpell);
        newParticleSystem.transform.position = position;
        newParticleSystem.transform.rotation = Quaternion.Euler(ang, tmp * 90, 0);
        NetworkServer.SpawnWithClientAuthority(newParticleSystem, base.connectionToClient);
        RpcSetFireParticleSpell(newParticleSystem);
    }
    [ClientRpc]
    public void RpcSetFireParticleSpell(GameObject x)
    {
        x.GetComponent<FireSpell>().PlayerNetId = this.netId;

        x.GetComponent<Laser>().damage = this.GetComponent<Combat>().heroDamage / 4;
        x.GetComponent<Laser>().damage = this.GetComponent<Combat>().heroDamage;
        if (isLocalPlayer)
        {
            GetComponent<ActionBar>().tmpSpell2 = x;
        }
    }
    [Command]
    public void CmdBubble()
    {
        RpcBubble();
    }
    [ClientRpc]
    public void RpcBubble()
    {
        this.GetComponent<Combat>().manaRegen *= 3;
        GameObject tmp = Instantiate(_bubble);
        tmp.transform.position = this.transform.position;
        tmp.GetComponent<BubbleSpellScript>().player = this.gameObject;
        NetworkServer.SpawnWithClientAuthority(tmp, base.connectionToClient);
    }
    /* [Command]
     public void CmdFire(Vector3 mousePosition, Vector3 playerPosition)
     {
         Vector2 velocity = mousePosition - playerPosition;


         Vector3 dir = mousePosition - playerPosition;
         float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;



         velocity.Normalize();
         GameObject f1 = Instantiate(bulet);
         GameObject f2 = Instantiate(bulet);
         GameObject f3 = Instantiate(bulet);
        // f1.transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
         //   float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
       //  f1.transform.Rotate(new Vector3(0, 0, ang));

         float offset2;
         offset2 = 0.5f;
         float tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
         float tmpy = offset2 / tmph * velocity.y;
         float tmpx = offset2 / tmph * velocity.x;

         f1.transform.position = new Vector3(playerPosition.x + tmpx, playerPosition.y + tmpy, 0);
         f2.transform.position = new Vector3(playerPosition.x + tmpx*1.5f, playerPosition.y + tmpy*1.5f, 0);
         f3.transform.position = new Vector3(playerPosition.x + tmpx*1f, playerPosition.y + tmpy * 1f, 0);
         //  Rigidbody2D n = f1.GetComponent("Rigidbody2D") as Rigidbody2D;
         f1.GetComponent<Rigidbody2D>().velocity= new Vector2(velocity.x * buletSpeed, velocity.y * buletSpeed);
         f2.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * buletSpeed+2, velocity.y * buletSpeed +2);
         f3.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * buletSpeed+4, velocity.y * buletSpeed+4 );

         GetComponent<Combat>().CmdManaUse(8);

         NetworkServer.Spawn(f1);
         NetworkServer.Spawn(f2);
         NetworkServer.Spawn(f3);
     }*/
    [Command]
    public void CmdFireTrail()
    {
        RpcFireTrail();
        GameObject tmp = Instantiate(_fireJumpTrail);
        tmp.transform.position = this.transform.position;
        NetworkServer.Spawn(tmp);
    }
    [ClientRpc]
    public void RpcFireTrail()
    {
        GetComponent<Movement>().jumpPower = 30;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
        GetComponent<Movement>().jumpPower = 12;
    }
    [Command]
    public void CmdFireball(Vector3 mousePosition, Vector3 playerPosition)
    {
        Vector2 velocity = mousePosition - playerPosition;

        Vector3 dir = mousePosition - playerPosition;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        velocity.Normalize();
        GameObject f = Instantiate(bulet);
        f.transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
        //   float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
        f.transform.Rotate(new Vector3(0, 0, ang));

        float offset2;
        offset2 = 0.5f;
        float tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        float tmpy = offset2 / tmph * velocity.y;
        float tmpx = offset2 / tmph * velocity.x;

        f.transform.position = new Vector3(playerPosition.x + tmpx, playerPosition.y + tmpy, 0);

        Rigidbody2D n = f.GetComponent("Rigidbody2D") as Rigidbody2D;
        n.velocity = velocity * buletSpeed;

        NetworkServer.Spawn(f);
        RpcFireball(f);
    }
    [ClientRpc]
    public void RpcFireball(GameObject x)
    {
        x.GetComponent<BuletMovement>().damage = this.GetComponent<Combat>().heroDamage *1.5f;
        x.GetComponent<BuletMovement>().PlayerNetId = this.netId;
    }

    [Command]
    public void CmdGranadePoison(Vector3 mousePosition, Vector3 playerPosition, float timeButtonPressLast)
    {
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = mousePosition - playerPosition;
        velocity.Normalize();
        GameObject f = Instantiate(granadePoison);

        float offset2 = 0.5f;
        float tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        float tmpy = offset2 / tmph * velocity.y;
        float tmpx = offset2 / tmph * velocity.x;
        f.transform.position = new Vector3(playerPosition.x + tmpx, playerPosition.y + tmpy, 0);

        Rigidbody2D n = f.GetComponent("Rigidbody2D") as Rigidbody2D;
        if (timeButtonPressLast > 2)
            timeButtonPressLast = 2;
        n.velocity = velocity * granadeSpeed * 2 * timeButtonPressLast * 2;

        float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;

        f.transform.Rotate(new Vector3(0, 0, ang));


        NetworkServer.Spawn(f);
        RpcPoison(f);
    }

    [ClientRpc]
    public void RpcPoison(GameObject x)
    {
        x.GetComponent<GranadeMovement>().spellType = 1;
        x.GetComponent<GranadeMovement>().damage = this.GetComponent<Combat>().heroDamage / 20;
        x.GetComponent<GranadeMovement>().PlayerNetId = this.netId;
    }

    [Command]
    public void CmdGranadeFrostNova(Vector3 mousePosition, Vector3 playerPosition, float timeButtonPressLast)
    {
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = mousePosition - playerPosition;
        velocity.Normalize();
        GameObject f = Instantiate(granadeFrostNova);

        float offset2 = 0.5f;
        float tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        float tmpy = offset2 / tmph * velocity.y;
        float tmpx = offset2 / tmph * velocity.x;
        f.transform.position = new Vector3(playerPosition.x + tmpx, playerPosition.y + tmpy, 0);

        Rigidbody2D n = f.GetComponent("Rigidbody2D") as Rigidbody2D;
        if (timeButtonPressLast > 2)
            timeButtonPressLast = 2;
        n.velocity = velocity * granadeSpeed * 2 * timeButtonPressLast * 2;

        float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;

        f.transform.Rotate(new Vector3(0, 0, ang));

        NetworkServer.Spawn(f);
        RpcFrostNova(f);
    }
    [ClientRpc]
    public void RpcFrostNova(GameObject x)
    {
        x.GetComponent<GranadeMovement>().spellType =2;
        x.GetComponent<GranadeMovement>().PlayerNetId = this.netId;
    }

    [Command]
    public void CmdPoisonDart(Vector3 mousePosition, Vector3 playerPosition)
    {
        Vector3 dir = mousePosition - playerPosition;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector2 velocity = mousePosition - playerPosition;

        velocity.Normalize();
        GameObject f = Instantiate(_poisonDart);

        //  float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
        // f.transform.Rotate(new Vector3(0, 0, ang));
        f.transform.rotation = Quaternion.AngleAxis(ang - 45f, Vector3.forward);
        f.GetComponent<PoisonDartMovement>().ang = ang;
        float offset2;
        offset2 = 0.5f;
        float tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        float tmpy = offset2 / tmph * velocity.y;
        float tmpx = offset2 / tmph * velocity.x;

        f.transform.position = new Vector3(playerPosition.x + tmpx, playerPosition.y + tmpy, 0);

        Rigidbody2D n = f.GetComponent("Rigidbody2D") as Rigidbody2D;
        n.velocity = velocity * buletSpeed;

        NetworkServer.Spawn(f);
        RpcSetNetId(f);
    }
    [ClientRpc]
    public void RpcSetNetId(GameObject x)
    {

        x.GetComponent<PoisonDartMovement>().damage = this.GetComponent<Combat>().heroDamage*1.2f;
        x.GetComponent<PoisonDartMovement>().PlayerNetId = this.netId;
    }



    public void FiremanTransformStart(int x)
    {
        GetComponent<Movement>().CmdChangeSkin(5);
        GetComponent<Combat>().Maxhealth *= 3;
        GetComponent<Combat>().heroDamage *= 2;
        GetComponent<Combat>().healthRegen *= 2;
        GetComponent<Combat>().health = GetComponent<Combat>().Maxhealth;
        //GetComponent<PlayerBuffs>().unlimitedManaStart();
        _transformSkin = x;
        Invoke("FiremanTransformEnd", 6f);
    }

    public void FiremanTransformEnd()
    {
        GetComponent<Combat>().heroDamage /= 2;
        GetComponent<Movement>().CmdChangeSkin(_transformSkin);
        GetComponent<Combat>().healthRegen /= 2;
        GetComponent<Combat>().Maxhealth /= 3;
        GetComponent<Combat>().health = GetComponent<Combat>().Maxhealth;
    }
    public void StunMe(float t)
    {
        GetComponent<Movement>().speed = 0f;
        Invoke("SpeedUpEnd", t);
    }
    public void StunMeEnd()
    {
        GetComponent<Movement>().speed = 0.5f;
    }
    public void SpeedUpStart()
    {
        GetComponent<Movement>().speed = 1.2f;
        Invoke("SpeedUpEnd", 5);
    }
    public void SpeedUpEnd()
    {
        GetComponent<Movement>().speed = 0.5f;
    }

    [Command]
    public void CmdInvisibleStart()
    {
        RpcInvisibleStart();

    }
    [ClientRpc]
    public void RpcInvisibleStart()
    {
        GetComponent<Combat>().manaRegen *= 2;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Combat>().isVisible = false;
    }

    [Command]
    public void CmdInvisibleEnd()
    {
        RpcInvisibleEnd();
    }
    [ClientRpc]
    public void RpcInvisibleEnd()
    {
        GetComponent<Combat>().manaRegen /= 2;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Combat>().isVisible = true;
    }


    void FrostNovaEnd()
    {
        this.gameObject.GetComponent<Movement>().speed = 0.5f;
        this.gameObject.GetComponent<Movement>().jumpPower = 12f;
    }

    [Command]
    public void CmdMagicShield(Vector3 mousePosition, Vector3 playerPosition, int x)
    {
        GameObject f;
        if (x == 1)
            f = Instantiate(MageShield);
        else if (x == 2)
            f = Instantiate(WarlockShield);
        else if (x == 3)
            f = Instantiate(FiremanShield);
        else
            f = Instantiate(ClericShield);


        Vector2 velocity = mousePosition - playerPosition;
        velocity.Normalize();

        float offset2 = 0.5f;
        float tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        float tmpy = offset2 / tmph * velocity.y;
        float tmpx = offset2 / tmph * velocity.x;
        f.transform.position = new Vector3(playerPosition.x + tmpx * 1.5f, playerPosition.y + tmpy * 1.5f, 0);
        int degrees = 0;


        float ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
        if (velocity.x > 0)
        {
            degrees = 180;
            ang = ang * (-1);
        }
        f.transform.Rotate(new Vector3(0, degrees, ang));

        NetworkServer.SpawnWithClientAuthority(f, base.connectionToClient);

        RpcSetMagicShield(f);
    }
    [ClientRpc]
    public void RpcSetMagicShield(GameObject x)
    {
        GetComponent<ActionBar>().tmpSpell1 = x;
    }
}
