using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Combat : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeHP")]
    public float health;
    [SyncVar(hook = "OnChangeMana")]
    public float mana;

    public float points;
    public float kills;
    public float assist;
    public float heroDamage;

    public float healthRegen;

    public float manaRegen;

    public float HealthBarDisplay;

    public float ManaBarDisplay;
    [SyncVar]
    public float MaxMana;
    [SyncVar]
    public float Maxhealth;

    public int xoffset;
    public int yoffset;

    public int textheight;
    public int textwidth;

    public bool isVisible;
    // public string PlayerName;
    [SyncVar(hook = "onPlayerNameChange")]
    public string PlayerName;
    [SyncVar(hook = "onHeroChange")]
    public int hero;

    public Texture2D StatusBar;
    public Texture2D HealthTexture;
    public Texture2D EnemyHealthBackground;
    public Texture2D ManaTexture;
    public Texture2D EnemyHealthTexture;
    public Texture2D TableTexture;
    // public Texture2D Spell1;
    // public Texture2D Spell2;
    // public Texture2D Spell3;
    // public Texture2D Spell4;

    Vector2 BarSize = new Vector2(250, 50);//size of hp and mana bar
    Vector2 InsideSize = new Vector2(165, 20);
    Vector2 EnemyBarSize = new Vector2(100, 20);//size of enempy hp bar

    public GUIStyle progressHealth_empty;
    public GUIStyle progressHealth_full;
    public GUIStyle progressMana_empty;
    public GUIStyle progressMana_full;
    public GUIStyle textStyle;
    // public GUIStyle actionBar;

    public float RegenHealthTimeWait;
    public float RegenHealthTimeLast;

    public float RegenManaTimeWait;
    public float RegenManaTimeLast;

    public List<Vector3> RespawnPositions = new List<Vector3>();
    public float RespawnTime;
    public bool IsDead;
    public NetworkInstanceId LastHitNetId;
    public NetworkInstanceId PlayerNetId;
    public int PlayerTeamId;
    public bool isEnd;
    public GameObject statisticTable;
    public GameObject killsText;
    public Text pointsText;

    private void Start()
    {
        isEnd = false;
        FloatingTextController.Initialize();
        PlayerName = PlayerName + netId.Value.ToString();
        PlayerNetId = netId;

        isVisible = true;
        kills = 0;
        points = 0;
        IsDead = false;
        // Maxhealth = 100;
        // MaxMana = 200;
        // mana = MaxMana;
        //  health = Maxhealth;
        //CmdSetAttributes();

        RespawnPositions.Add(new Vector3(-1f, 2f, 0));
        RespawnPositions.Add(new Vector3(3f, 3.2f, 0));
        RespawnPositions.Add(new Vector3(12f, 5f, 0));
        RespawnPositions.Add(new Vector3(-7, 2f, 0));
        RespawnPositions.Add(new Vector3(22f, 3f, 0));
        RespawnPositions.Add(new Vector3(-12f, 4f, 0));
    }
    [Command]
    public void CmdSetAttributes()
    {
        RpcSetAttributes();
    }
    [ClientRpc]
    public void RpcSetAttributes()
    {
        Debug.Log(GetComponent<Movement>().HeroId);
        if (GetComponent<Movement>().HeroId == 1)
        {
            heroDamage = 20;
            Maxhealth = 220;
            health = Maxhealth;
            MaxMana = 450;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 7;
            manaRegen = 15;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 4f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
        else if (GetComponent<Movement>().HeroId == 2)
        {
            heroDamage = 20;
            Maxhealth = 250;
            health = Maxhealth;
            MaxMana = 400;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 6;
            manaRegen = 15;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 5f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
        else if (GetComponent<Movement>().HeroId == 3)
        {
            heroDamage = 20;
            Maxhealth = 300;
            health = Maxhealth;
            MaxMana = 350;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 4;
            manaRegen = 12;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 5f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
        else if (GetComponent<Movement>().HeroId == 4)
        {
            heroDamage = 20;
            Maxhealth = 250;
            health = Maxhealth;
            MaxMana = 400;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 8;
            manaRegen = 17;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 3.5f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
    }
    public void OnChangeHP(float newHp)
    {
        health = newHp;
        CmdBar();
    }
    public void OnChangeMana(float newMana)
    {
        mana = newMana;
        CmdBar();
    }
    private void OnGUI()
    {

      //  if (!isEnd)
            if (isLocalPlayer)
            {

                xoffset = Screen.width - 250;
                yoffset = Screen.height - 50;

                //Player health bar
                GUI.BeginGroup(new Rect(0, 0 + yoffset, BarSize.x, BarSize.y), StatusBar, progressHealth_empty);
                GUI.Box(new Rect(0, 0 + yoffset, BarSize.x, BarSize.y), StatusBar, progressHealth_full);

                textStyle.fontStyle = FontStyle.Bold;

                GUI.BeginGroup(new Rect(43, 15, InsideSize.x * HealthBarDisplay, InsideSize.y));
                GUI.Box(new Rect(0, 0, InsideSize.x, InsideSize.y), HealthTexture, progressHealth_full);
                GUI.EndGroup();

                GUI.Label(new Rect(90, 16, 100, 20), (int)health + " / " + Maxhealth, textStyle);
                GUI.EndGroup();

                //Player mana bar
                GUI.BeginGroup(new Rect(0 + xoffset, 0 + yoffset, BarSize.x, BarSize.y), StatusBar, progressMana_empty);
                GUI.Box(new Rect(0 + xoffset, 0 + yoffset, BarSize.x, BarSize.y), StatusBar, progressMana_full);

                GUI.BeginGroup(new Rect(43, 15, InsideSize.x * ManaBarDisplay, InsideSize.y));
                GUI.Box(new Rect(0, 0, InsideSize.x, InsideSize.y), ManaTexture, progressMana_full);

                GUI.EndGroup();
                GUI.Label(new Rect(90, 16, 100, 20), mana + " / " + MaxMana, textStyle);
                GUI.EndGroup();

            }
            else
            {
                //Enemy health bar above head
                if (isVisible)
                {
                    Vector2 tmp = Camera.main.WorldToScreenPoint(this.transform.position);
                    GUI.Label(new Rect(tmp.x - 50, Screen.height - tmp.y - 130, 100, 30), this.PlayerName);

                    GUI.BeginGroup(new Rect(tmp.x - 50, Screen.height - tmp.y - 100, EnemyBarSize.x, EnemyBarSize.y), EnemyHealthBackground, progressHealth_empty);
                    GUI.Box(new Rect(0, 0, EnemyBarSize.x, EnemyBarSize.y), EnemyHealthBackground, progressHealth_full);

                    GUI.BeginGroup(new Rect(0, 0, EnemyBarSize.x * this.HealthBarDisplay, EnemyBarSize.y));
                    GUI.Box(new Rect(0, 0, EnemyBarSize.x, EnemyBarSize.y), EnemyHealthTexture, progressHealth_full);

                    GUI.EndGroup();
                    GUI.EndGroup();
                }

            }
      /*  if (isEnd)
        {
            GUI.BeginGroup(new Rect(50, 50, 500, 500));
            GUI.DrawTexture(new Rect(0, 0, 480, 480), TableTexture);
        }*/

    }



    void CmdBar()
    {
        HealthBarDisplay = health / Maxhealth;
        ManaBarDisplay = mana / MaxMana;
    }
    [Command]
    public void CmdDestroyObjects()
    {
        RpcDestroyObjects();
    }
    [ClientRpc]
    public void RpcDestroyObjects()
    {
        if (this.GetComponent<ActionBar>().tmpSpell1 != null)
        { }
        if (this.GetComponent<ActionBar>().tmpSpell2 != null)
        {
            this.GetComponent<ActionBar>().tmpSpell2.GetComponent<Laser>().CmdDestroyMe(4);
            this.GetComponent<ActionBar>().tmpSpell2.GetComponent<Laser>().CmdStopEmission();
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {   
            if (RegenHealthTimeLast + RegenHealthTimeWait < Time.time)
            {
                HealthRegeneration();
                RegenHealthTimeLast = Time.time;
            }
            if (RegenManaTimeLast + RegenManaTimeWait < Time.time)
            {
                ManaRegeneration();
                RegenManaTimeLast = Time.time;
            }
            if (mana > MaxMana)
                mana = MaxMana;
            if (health > Maxhealth)
                health = Maxhealth;
            CmdBar();
            if (health <= 0)
            {
                // isEnd = true;
                Debug.Log(LastHitNetId);

                CmdNetworkServerAddKills();
                FloatingTextController.CreateFloatingText("YOU ARE DEAD!", this.transform, 0, 0);
                //  this.gameObject.SetActive(false);

               GameObject tmp= Instantiate(statisticTable);
                tmp.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-0.5f,0);

                //  killsText= statisticTable.GetComponent<Canvas>().GetComponent<UnityEngine.UI.Text>();
                GameObject StatisticTable = GameObject.FindGameObjectWithTag("StatisticTable");
                Transform canvasObject = tmp.transform.Find("Canvas");
               
                Transform textTr = canvasObject.transform.Find("TextPoints");
                Text text = textTr.GetComponent<Text>();
                text.text = points.ToString();

                Transform textTr2 = canvasObject.transform.Find("TextKills");
                Text text2 = textTr2.GetComponent<Text>();
                text2.text = kills.ToString();

                Transform textTr3 = canvasObject.transform.Find("TextName");
                Text text3 = textTr3.GetComponent<Text>();
                text3.text = PlayerName.ToString();
               
                CmdSetPlayerVisibility(false);
               
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                 Invoke("CmdRespawn", 3);

            }
            if (mana < 1)
                mana = 0;
        }
    }
    [Command]
    public void CmdNetworkServerAddKills()
    {
        NetworkServer.FindLocalObject(LastHitNetId).GetComponent<Combat>().CmdSetKill();
    }
    [Command]
    public void CmdSetPlayerVisibility(bool x)
    {
        RpcSetPlayerVisibility(x);
        RpcDestroyObjects();
    }
    [ClientRpc]
    public void RpcSetPlayerVisibility(bool x)
    {
        //  GetComponent<Movement>().player.active = x;
        this.gameObject.SetActive(x);

    }

    void HealthRegeneration()
    {
        Debug.Log(LastHitNetId);
        //  Debug.Log("Points: " + points);
        // Debug.Log("Kills: " + kills);
        health += 1 * healthRegen;
        FloatingTextController.CreateGreenFloatingText("+" + healthRegen.ToString(), this.transform, Screen.width / 2 - 100, Screen.height / 2 - 90);
    }

    void ManaRegeneration()
    {
        mana += 1 * manaRegen;
        FloatingTextController.CreateBlueFloatingText("+" + manaRegen.ToString(), this.transform, Screen.width / 2 - 100, Screen.height / 2 - 90);
    }



    [Command]
    public void CmdSetPoints(float x)
    {
        RpcSetPoints(x);
    }
    [ClientRpc]
    public void RpcSetPoints(float x)
    {
        this.points += x;
    }
    [Command]
    public void CmdSetKill()
    {
        RpcSetKill();
    }
    [ClientRpc]
    public void RpcSetKill()
    {
        Debug.Log("Kill++++++");
        this.kills++;
    }

    [Command]
    public void CmdHit(float dmg,NetworkInstanceId x)
    {
        RpcHit(dmg,x);

    }
    [ClientRpc]
    public void RpcHit(float dmg,NetworkInstanceId x)
    {
        health -= dmg;
        LastHitNetId = x;
    }

  //  [Command]
    public void hit(float dmg)
    {

        //   if (!isServer)
        //   return;

        RpcHit(dmg);

        //  if (health ==0)
        //    NetworkServer.FindLocalObject(LastHitNetId).GetComponent<Combat>().CmdSetKill();
    }
   // [ClientRpc]
    public void RpcHit(float dmg)
    {
        health -= dmg;
       
    }
    // [Command]
    public void CmdManaUp(float manaUpTmp)
    {
        mana += manaUpTmp;
        FloatingTextController.CreateBlueFloatingText("+" + manaUpTmp.ToString(), this.transform, Screen.width / 2 - 100, Screen.height / 2 - 90);
    }
    [Command]
    public void CmdHealMe(float healPoints)
    {
        health += healPoints;
        FloatingTextController.CreateGreenFloatingText("+" + healPoints.ToString(), this.transform, Screen.width / 2 - 100, Screen.height / 2 - 90);
    }
    /* public void healthBuffDurationStart()
     {

         health *= 2;
         Maxhealth *= 2;
         Invoke("healthBuffDurationEnd", 5);
     }
     public void healthBuffDurationEnd()
     {
         health /= 2;
         Maxhealth /= 2;
         GetComponent<Movement>().buffed = false;
     }*/

    [Command]
    void CmdDestroy(GameObject x)
    {
        Debug.Log("Unisti me CMD");
        Destroy(x);
    }

    [Command]
    public void CmdManaUse(float x)
    {
        //  if (mana - x >= 0)
        mana -= x;
    }

    [Command]
    void CmdRespawn()
    {
        System.Random rnd = new System.Random();
        transform.position = RespawnPositions[rnd.Next(0,5)];
        CmdSetPlayerVisibility(true);
        // health = Maxhealth;
        CmdSetAttributes();
        isEnd = false;
        //  mana = MaxMana;
    }
    public void SetAtributes()
    {
        Debug.Log(GetComponent<Movement>().HeroId);
        if (GetComponent<Movement>().HeroId == 1)
        {
            heroDamage = 20;
            Maxhealth = 220;
            health = Maxhealth;
            MaxMana = 450;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 7;
            manaRegen = 15;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 4f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
        else if (GetComponent<Movement>().HeroId == 2)
        {
            heroDamage = 20;
            Maxhealth = 250;
            health = Maxhealth;
            MaxMana = 400;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 6;
            manaRegen = 15;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 5f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
        else if (GetComponent<Movement>().HeroId == 3)
        {
            heroDamage = 20;
            Maxhealth = 300;
            health = Maxhealth;
            MaxMana = 350;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 4;
            manaRegen = 12;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 5f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
        else if (GetComponent<Movement>().HeroId == 4)
        {
            heroDamage = 20;
            Maxhealth = 250;
            health = Maxhealth;
            MaxMana = 400;
            mana = MaxMana;
            HealthBarDisplay = health / Maxhealth;
            ManaBarDisplay = mana / MaxMana;
            healthRegen = 8;
            manaRegen = 17;
            RegenHealthTimeLast = Time.time;
            RegenHealthTimeWait = 3.5f;
            RegenManaTimeWait = 3f;
            RegenManaTimeLast = Time.time;
        }
    }
    public void onPlayerNameChange(string newName)
    {
        Debug.Log("AAAAAA" + newName);
        PlayerName = newName;
    }
    public void onHeroChange(int newHero)
    {
        Debug.Log("hero=" + newHero);
        hero = newHero;
        GetComponent<Movement>().HeroId = newHero;
        this.GetComponent<Movement>().changeSkin(hero);
        SetAtributes();
        Debug.Log("BBBBBBBBBB" + GetComponent<Movement>().HeroId);
    }
}
