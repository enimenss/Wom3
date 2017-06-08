using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class Movement : NetworkBehaviour
{
 
    Animator anim;
    public float speed;
    public float movx;
    public float movy;
    public bool jumpFlag = false;
    public bool jumpFlag2 = false;//double jump
    public GameObject player;
    public float jumpPower;

    public GameObject buffCreator;
    public GameObject cloud;

    // public int HeroId;
    [SyncVar(hook = "oneHeroChange")]
    public int HeroId;

    private Rigidbody2D r;
    private float cooldown;
    private float timeButtonPressed;
    public float timeButtonPressLast;

    private float coolTime;

    public bool isGrounded;

    void Start()
    {
       // HeroId = 1;
        r = player.GetComponent<Rigidbody2D>();
        cooldown = 1f;
        coolTime = 0f;
        anim = GetComponent<Animator>();
        buffCreator = GameObject.FindGameObjectWithTag("WorldBuffCreator");
    }
    [Command]
    public void CmdChangeSkin(int x)
    {
        RpcChangeSkin(x);
    }
    [ClientRpc]
    public void RpcChangeSkin(int x)
    {
        if (x == 1)
        {

            RuntimeAnimatorController tmp = Resources.Load("MageAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
            HeroId = 1;
            GetComponent<ActionBar>().spell1PressName = "MageSpell1Press";
            GetComponent<ActionBar>().spell2PressName = "MageSpell2Press";
            GetComponent<ActionBar>().spell2DownName = "MageSpell2Down"; ;
            GetComponent<ActionBar>().spell2UpName = "MageSpell2Up"; ;
            GetComponent<ActionBar>().spell3PressName = "MageSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "MageSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "MageSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "MageSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "MageSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "MageSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "MageSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "MageSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 10f;
            GetComponent<ActionBar>().cooldown2 = 10f;
            GetComponent<ActionBar>().cooldown3 = 35f;
            GetComponent<ActionBar>().cooldown4 = 20f;

            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().MageSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().MageSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().MageSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().MageSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().MageSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().MageSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().MageSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().MageSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().MageSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().MageSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().MageSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().MageSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if (x == 2)
        {
            RuntimeAnimatorController tmp = Resources.Load("WarlockAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
            HeroId = 2;
            GetComponent<ActionBar>().spell1PressName = "WarlockSpell1Press";
            GetComponent<ActionBar>().spell2PressName = "WarlockSpell2Press";
            GetComponent<ActionBar>().spell2DownName = ""; ;
            GetComponent<ActionBar>().spell2UpName = "WarlockSpell2Up"; ;
            GetComponent<ActionBar>().spell3PressName = "WarlockSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "WarlockSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "WarlockSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "WarlockSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "WarlockSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "WarlockSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "WarlockSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "WarlockSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 15f;
            GetComponent<ActionBar>().cooldown2 = 10f;
            GetComponent<ActionBar>().cooldown3 = 30f;
            GetComponent<ActionBar>().cooldown4 = 20f;
            
            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().WarlockSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().WarlockSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().WarlockSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().WarlockSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().WarlockSpell5; 
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().WarlockSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().WarlockSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().WarlockSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().WarlockSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().WarlockSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().WarlockSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().WarlockSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if(x==3)
        {
            RuntimeAnimatorController tmp = Resources.Load("FiremanAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
            HeroId = 3;
            GetComponent<ActionBar>().spell1PressName = "FiremanSpell1Press";
            GetComponent<ActionBar>().spell2PressName = "FiremanSpell2Press";
            GetComponent<ActionBar>().spell2DownName = "FiremanSpell2Down"; ;
            GetComponent<ActionBar>().spell2UpName = "FiremanSpell2Up"; ;
            GetComponent<ActionBar>().spell3PressName = "FiremanSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "FiremanSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "FiremanSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "FiremanSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "FiremanSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "FiremanSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "FiremanSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "FiremanSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 12f;
            GetComponent<ActionBar>().cooldown2 = 15f;
            GetComponent<ActionBar>().cooldown3 = 35f;
            GetComponent<ActionBar>().cooldown4 = 20f;


            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().FiremanSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().FiremanSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().FiremanSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().FiremanSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().FiremanSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().FiremanSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().FiremanSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().FiremanSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().FiremanSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().FiremanSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().FiremanSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().FiremanSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if(x==4)
        {
            RuntimeAnimatorController tmp = Resources.Load("ClericAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
            HeroId = 4;
            GetComponent<ActionBar>().spell1PressName = "ClericSpell1Press";
            GetComponent<ActionBar>().spell2PressName = "ClericSpell2Press";
            GetComponent<ActionBar>().spell2DownName = ""; ;
            GetComponent<ActionBar>().spell2UpName = ""; ;
            GetComponent<ActionBar>().spell3PressName = "ClericSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "ClericSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "ClericSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "ClericSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "ClericSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "ClericSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "ClericSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "ClericSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 10f;
            GetComponent<ActionBar>().cooldown2 = 15f;
            GetComponent<ActionBar>().cooldown3 = 35f;
            GetComponent<ActionBar>().cooldown4 = 20f;

            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().ClericSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().ClericSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().ClericSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().ClericSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().ClericSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().ClericSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().ClericSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().ClericSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().ClericSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().ClericSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().ClericSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().ClericSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if (x==5)
        {
            RuntimeAnimatorController tmp = Resources.Load("FiremanTransform") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;

        }
        Debug.Log(GetComponent<ActionBar>().spell1PressName);
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                CmdChangeSkin(1);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                CmdChangeSkin(2);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                CmdChangeSkin(3);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                CmdChangeSkin(4);
              
            }
            playerMove();
            if (Input.GetKey(KeyCode.A))
            {
                // button2pressed = true;
                anim.SetInteger("State", 2);
                //  if (Input.GetKeyDown(KeyCode.D))
                //    anim.SetTrigger("RotateRight");

                // GetComponent<Transform>().localScale.Set(-1, 0, 0);

            }
            if (Input.GetKey(KeyCode.D))
            {
                //  GetComponent<Transform>().localScale.Set(1, 0, 0);

                // button1pressed = true;
                anim.SetInteger("State", 1);
                //  if (Input.GetKeyDown(KeyCode.A))
                //   anim.SetTrigger("RotateLeft");
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                //  button2pressed = false;
                //  if (!button1pressed)
                {
                    //  anim.ResetTrigger("RotateRight");
                    anim.SetInteger("State", 0);
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                // button1pressed = false;
                // if (!button2pressed)
                {
                    // anim.ResetTrigger("RotateLeft");
                    anim.SetInteger("State", 0);

                }
            }
            /*  if (Input.GetMouseButtonDown(0))
              {
                  // if (Time.time > coolTime + cooldown)
                  // {
                  if (GetComponent<Combat>().mana - 8 >= 0)
                  {
                      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                      GetComponent<Spells>().CmdFire(mousePosition, this.transform.position);

                  }
                  // coolTime = Time.time;
                  //  }
              }*/
           /* if (Input.GetKeyUp(KeyCode.F))
            {
                timeButtonPressLast = Time.time - timeButtonPressed;
                if (Time.time > coolTime + cooldown)
                {
                    if (GetComponent<Combat>().mana - 15 >= 0)
                    {
                        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        GetComponent<Spells>().CmdGranade(mousePosition, this.transform.position,timeButtonPressLast);
                        coolTime = Time.time;

                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                timeButtonPressed = Time.time;
                 if (Time.time > coolTime + cooldown)
                 {
                     CmdGranade();
                     coolTime = Time.time;
                 }
            }*/

            if (Input.GetKeyDown(KeyCode.S))
            {
                playerDuck();
                anim.SetTrigger("Duck");
            }

            if (player.gameObject.transform.position.y < -5)
            {
                ResetPosition();
            }
        }
    }

    void OnCollisionStay2D(Collision2D x)
    {

        if (x.gameObject.tag == "Platform")
        {
            jumpFlag = true;
            jumpFlag2 = true;
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }

    }
    void OnCollisionExit2D(Collision2D x)
    {
        if (x.gameObject.tag == "Platform")
        {
            jumpFlag = false;
            isGrounded = false;
            anim.SetBool("isGrounded", false);
        }
    }
    void ResetPosition()
    {
        player.transform.position = new Vector3(0, 1.5f, 0);
    }
    void playerMove()
    {
        movx = Input.GetAxis("Horizontal");
        if ((jumpFlag2 || jumpFlag) && Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpFlag == false)
            {
                CmdCloudInstantate();
                jumpFlag2 = false;
            }
            jumpFlag = false;
            movy = 1;

        }
        r.AddForce(new Vector2(movx * speed, movy * jumpPower), ForceMode2D.Impulse);
        movy = 0;
    }

    [Command]
    void CmdCloudInstantate()
    {
        GameObject _cloud = Instantiate(cloud);
        _cloud.transform.position = this.transform.position;
        NetworkServer.Spawn(_cloud);
    }

    void playerDuck()
    {
        movy = 1;
        r.AddForce(-(new Vector2(0, movy * jumpPower)), ForceMode2D.Impulse);
        movy = 0;
    }

    void OnTriggerEnter2D(Collider2D x)
    {
        if (isLocalPlayer)
        {

            if (x.gameObject.tag == "ManaUp")
            {
                if (GetComponent<Combat>().mana != GetComponent<Combat>().MaxMana)
                {
                    FloatingTextController.CreateBlueFloatingText("+40", x.gameObject.transform,0,0);
                    GetComponent<Combat>().CmdManaUp(40);
                    CmdDestroyIt(x.gameObject);

                }
            }

            if (x.gameObject.tag == "Heal")
            {
                if (GetComponent<Combat>().health != GetComponent<Combat>().Maxhealth)
                {
                    FloatingTextController.CreateGreenFloatingText("+30", x.gameObject.transform, 0, 0);
                    GetComponent<Combat>().CmdHealMe(30);
                    CmdDestroyIt(x.gameObject);
                  
                }
            }

            if (x.gameObject.tag == "Buffhp")
            {
                if (!GetComponent<PlayerBuffs>().buffedHp)
                {
                    FloatingTextController.CreateFloatingText("Health points are doubled", this.transform, 0, Screen.height / 2 - 150);
                    GetComponent<PlayerBuffs>().buffedHpStart();
                    CmdDestroyIt(x.gameObject);
                   
                }
            }
            if (x.gameObject.tag == "BuffUnlimitedMana")
            {
                if (!GetComponent<PlayerBuffs>().unlimitedMana)
                {
                    FloatingTextController.CreateFloatingText("Unlimited mana", this.transform, 0, Screen.height / 2 - 150);
                    GetComponent<PlayerBuffs>().unlimitedManaStart();
                    CmdDestroyIt(x.gameObject);

                }
            }
            if (x.gameObject.tag == "BuffDoubleRegen")
            {
                if (!GetComponent<PlayerBuffs>().doubleRegen)
                {
                    FloatingTextController.CreateFloatingText("Regeneration is doubled", this.transform, 0, Screen.height / 2 - 150);
                    GetComponent<PlayerBuffs>().doubleRegenStart();
                    CmdDestroyIt(x.gameObject);

                }
            }
        }
    }
    [Command]
    public void CmdDestroyIt(GameObject x)
    {
        NetworkServer.Destroy(x);
    }
    public void changeSkin(int x)
    {
        Debug.Log("change SKIN ID: " + x);
        if (x == 1)
        {

            RuntimeAnimatorController tmp = Resources.Load("MageAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
          //  HeroId = 1;
            GetComponent<ActionBar>().spell1PressName = "MageSpell1Press";
            
            GetComponent<ActionBar>().spell2PressName = "MageSpell2Press";
            GetComponent<ActionBar>().spell2DownName = "MageSpell2Down"; ;
            GetComponent<ActionBar>().spell2UpName = "MageSpell2Up"; ;
            GetComponent<ActionBar>().spell3PressName = "MageSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "MageSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "MageSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "MageSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "MageSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "MageSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "MageSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "MageSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 10f;
            GetComponent<ActionBar>().cooldown2 = 10f;
            GetComponent<ActionBar>().cooldown3 = 35f;
            GetComponent<ActionBar>().cooldown4 = 20f;

            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().MageSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().MageSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().MageSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().MageSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().MageSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().MageSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().MageSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().MageSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().MageSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().MageSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().MageSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().MageSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if (x == 2)
        {
            RuntimeAnimatorController tmp = Resources.Load("WarlockAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
           // HeroId = 2;
            GetComponent<ActionBar>().spell1PressName = "WarlockSpell1Press";
            GetComponent<ActionBar>().spell2PressName = "WarlockSpell2Press";
            GetComponent<ActionBar>().spell2DownName = ""; ;
            GetComponent<ActionBar>().spell2UpName = "WarlockSpell2Up"; ;
            GetComponent<ActionBar>().spell3PressName = "WarlockSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "WarlockSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "WarlockSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "WarlockSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "WarlockSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "WarlockSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "WarlockSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "WarlockSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 15f;
            GetComponent<ActionBar>().cooldown2 = 10f;
            GetComponent<ActionBar>().cooldown3 = 30f;
            GetComponent<ActionBar>().cooldown4 = 20f;

            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().WarlockSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().WarlockSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().WarlockSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().WarlockSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().WarlockSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().WarlockSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().WarlockSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().WarlockSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().WarlockSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().WarlockSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().WarlockSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().WarlockSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if (x == 3)
        {
            RuntimeAnimatorController tmp = Resources.Load("FiremanAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
           // HeroId = 3;
            GetComponent<ActionBar>().spell1PressName = "FiremanSpell1Press";
            Debug.Log("spell "+GetComponent<ActionBar>().spell1PressName);
            GetComponent<ActionBar>().spell2PressName = "FiremanSpell2Press";
            GetComponent<ActionBar>().spell2DownName = "FiremanSpell2Down"; ;
            GetComponent<ActionBar>().spell2UpName = "FiremanSpell2Up"; ;
            GetComponent<ActionBar>().spell3PressName = "FiremanSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "FiremanSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "FiremanSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "FiremanSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "FiremanSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "FiremanSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "FiremanSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "FiremanSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 12f;
            GetComponent<ActionBar>().cooldown2 = 15f;
            GetComponent<ActionBar>().cooldown3 = 35f;
            GetComponent<ActionBar>().cooldown4 = 20f;

            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().FiremanSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().FiremanSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().FiremanSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().FiremanSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().FiremanSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().FiremanSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().FiremanSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().FiremanSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().FiremanSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().FiremanSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().FiremanSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().FiremanSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if (x == 4)
        {
            RuntimeAnimatorController tmp = Resources.Load("ClericAnimator") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;
          //  HeroId = 4;
            GetComponent<ActionBar>().spell1PressName = "ClericSpell1Press";
            GetComponent<ActionBar>().spell2PressName = "ClericSpell2Press";
            GetComponent<ActionBar>().spell2DownName = ""; ;
            GetComponent<ActionBar>().spell2UpName = ""; ;
            GetComponent<ActionBar>().spell3PressName = "ClericSpell3Press";
            GetComponent<ActionBar>().spell4PressName = "ClericSpell4Press"; ;
            GetComponent<ActionBar>().spellLCPressName = "ClericSpellLCPress";
            GetComponent<ActionBar>().spellLCDownName = "ClericSpellLCDown";
            GetComponent<ActionBar>().spellLCUpName = "ClericSpellLCUp";
            GetComponent<ActionBar>().spellRCPressName = "ClericSpellRCPress";
            GetComponent<ActionBar>().spellRCDownName = "ClericSpellRCDown";
            GetComponent<ActionBar>().spellRCUpName = "ClericSpellRCUp";
            GetComponent<ActionBar>().cooldown1 = 10f;
            GetComponent<ActionBar>().cooldown2 = 15f;
            GetComponent<ActionBar>().cooldown3 = 35f;
            GetComponent<ActionBar>().cooldown4 = 20f;

            GetComponent<ActionBar>().Spell1Texture = GetComponent<SpellTextures>().ClericSpell1;
            GetComponent<ActionBar>().Spell2Texture = GetComponent<SpellTextures>().ClericSpell2;
            GetComponent<ActionBar>().Spell3Texture = GetComponent<SpellTextures>().ClericSpell3;
            GetComponent<ActionBar>().Spell4Texture = GetComponent<SpellTextures>().ClericSpell4;
            GetComponent<ActionBar>().Spell5Texture = GetComponent<SpellTextures>().ClericSpell5;
            GetComponent<ActionBar>().Spell6Texture = GetComponent<SpellTextures>().ClericSpell6;
            GetComponent<ActionBar>().Spell1DownTexture = GetComponent<SpellTextures>().ClericSpell1Pressed;
            GetComponent<ActionBar>().Spell2DownTexture = GetComponent<SpellTextures>().ClericSpell2Pressed;
            GetComponent<ActionBar>().Spell3DownTexture = GetComponent<SpellTextures>().ClericSpell3Pressed;
            GetComponent<ActionBar>().Spell4DownTexture = GetComponent<SpellTextures>().ClericSpell4Pressed;
            GetComponent<ActionBar>().Spell5DownTexture = GetComponent<SpellTextures>().ClericSpell5Pressed;
            GetComponent<ActionBar>().Spell6DownTexture = GetComponent<SpellTextures>().ClericSpell6Pressed;

            GetComponent<ActionBar>().SetTextureStyle();
            GetComponent<Combat>().CmdSetAttributes();
        }
        else if (x == 5)
        {
            RuntimeAnimatorController tmp = Resources.Load("FiremanTransform") as RuntimeAnimatorController;
            player.GetComponent<Animator>().runtimeAnimatorController = tmp;

        }

    }
    public void oneHeroChange(int heroChange)
    {
        Debug.Log("movment hero " + heroChange);
        HeroId = heroChange;
        changeSkin(HeroId);
        GetComponent<Combat>().CmdSetAttributes();
    }
}