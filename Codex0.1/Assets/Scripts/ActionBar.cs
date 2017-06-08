using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ActionBar : NetworkBehaviour
{

    #region Attributes
    public Font myFont;

    public string spell1PressName;
    public string spell2PressName;
    public string spell3PressName;
    public string spell4PressName;
    public string spellLCPressName;
    public string spellRCPressName;
    // public string spell1DownName;
    public string spell2DownName;
    // public string spell3DownName;
    // public string spell4DownName;
    public string spellLCDownName;
    public string spellRCDownName;
    // public string spell1UpName;
    public string spell2UpName;
    // public string spell3UpName;
    // public string spell4UpName;
    public string spellLCUpName;
    public string spellRCUpName;

    public float cooldown1;
    public float cooldown2;
    public float cooldown3;
    public float cooldown4;
    //  private float cooldownLC;
    //  private float cooldownRC;
    private float coolTime1;
    private float coolTime2;
    private float coolTime3;
    private float coolTime4;
    //  private float coolTimeLC;
    //  private float coolTimeRC;

    private float timeButtonPressed;
    public float timeButtonPressLast;

    private bool _isPressed;
    private bool _isPressedQ;
    private bool _isPressedW;
    private bool _isPressedE;
    private bool _isPressedR;
    private bool _isPressedLC;
    private bool _isPressedRC;

    public int xoffset;
    public int yoffset;

    public Texture2D Spell1Texture;
    public Texture2D Spell1DownTexture;
    public Texture2D Spell2Texture;
    public Texture2D Spell2DownTexture;
    public Texture2D Spell3Texture;
    public Texture2D Spell3DownTexture;
    public Texture2D Spell4Texture;
    public Texture2D Spell4DownTexture;
    public Texture2D Spell5Texture;
    public Texture2D Spell5DownTexture;
    public Texture2D Spell6Texture;
    public Texture2D Spell6DownTexture;
    public Texture2D SpellEmptyTexture;
    public Texture2D SpellLockedTexture;
    public Texture2D ActionBarTexture;
    public Texture2D cooldownTexture;


    /// ////////////////////////
    /* private Color color;
     public float progress;
     public float oldProg;
     private Texture2D progTex;*/


    public GUIStyle Spell1Style;
    public GUIStyle Spell2Style;
    public GUIStyle Spell3Style;
    public GUIStyle Spell4Style;
    public GUIStyle Spell5Style;
    public GUIStyle Spell6Style;

    //Pomocne promenljive
    public GameObject tmpSpell1;
    public GameObject tmpSpell2;
    Vector3 mousePosition;
    Vector2 velocity;

    public GUIStyle actionBar;

    float offset2;
    float tmph;
    float tmpy;
    float tmpx;
    float ang;
    float tmpY;
    int degrees;
    #endregion

    void Start()
    {
        ///////////////////////////
        /*  color = new Color(1, 1, 1);
          progress = 0.92f;
          oldProg = 0f;
          progTex = ProgressUpdate(progress, color);*/
        spell1PressName = "MageSpell1Press";
        spell2PressName = "MageSpell2Press";
        spell2DownName = "MageSpell2Down"; ;
        spell2UpName = "MageSpell2Up"; ;
        spell3PressName = "MageSpell3Press";
        spell4PressName = "MageSpell4Press"; ;
        spellLCPressName = "MageSpellLCPress";
        spellLCDownName = "MageSpellLCDown";
        spellLCUpName = "MageSpellLCUp";
        spellRCPressName = "MageSpellRCPress";
        spellRCDownName = "MageSpellRCDown";
        spellRCUpName = "MageSpellRCUp";
        cooldown1 = 10f;
        cooldown2 = 10f;
        cooldown3 = 35f;
        cooldown4 = 20f;

        _isPressed = false;
        _isPressedQ = false;
        _isPressedW = false;
        _isPressedE = false;
        _isPressedR = false;
        _isPressedLC = false;
        _isPressedRC = false;

        Spell1Style.normal.background = Spell1Texture;
        Spell2Style.normal.background = Spell2Texture;
        Spell3Style.normal.background = Spell3Texture;
        Spell4Style.normal.background = Spell4Texture;
        Spell5Style.normal.background = Spell5Texture;
        Spell6Style.normal.background = Spell6Texture;
    }
    public void SetTextureStyle()
    {
        Spell1Style.normal.background = Spell1Texture;
        Spell2Style.normal.background = Spell2Texture;
        Spell3Style.normal.background = Spell3Texture;
        Spell4Style.normal.background = Spell4Texture;
        Spell5Style.normal.background = Spell5Texture;
        Spell6Style.normal.background = Spell6Texture;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            /* 
              if (oldProg != progress)
              {
                  progTex = ProgressUpdate(progress, color);
                  oldProg = progress;
              }*/

            #region UpdatingVariables
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = mousePosition - this.transform.position;
            velocity.Normalize();
            offset2 = 0.5f;
            tmph = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
            tmpy = offset2 / tmph * velocity.y;
            tmpx = offset2 / tmph * velocity.x;

            ang = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
            tmpY = 1;
            degrees = 0;
            if (velocity.x < 0)
            {
                tmpY = -1;

            }
            else
                ang = ang * (-1);
            #endregion

            #region ButtonLC
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isPressed)
                {
                    _isPressedLC = true;
                    _isPressed = true;
                    SpellLCPress();
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (_isPressedLC && _isPressed)
                    SpellLCDown();
                Spell5Style.normal.background = Spell5DownTexture;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (_isPressedLC && _isPressed)
                {
                    _isPressed = false;
                    _isPressedLC = false;
                    SpellLCUp();
                }
                Spell5Style.normal.background = Spell5Texture;
            }
            #endregion

            #region ButtonRC
            if (Input.GetMouseButtonDown(1))
            {
                if (!_isPressed)
                {
                    _isPressedRC = true;
                    _isPressed = true;
                    SpellRCPress();
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (_isPressedRC && _isPressed)
                    SpellRCDown();
                Spell6Style.normal.background = Spell6DownTexture;
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (_isPressedRC && _isPressed)
                {
                    _isPressedRC = false;
                    _isPressed = false;
                    SpellRCUp();
                }
                Spell6Style.normal.background = Spell6Texture;
            }
            #endregion

            #region ButtonQ
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!_isPressed)
                    if (Time.time > cooldown1 + coolTime1)
                    {
                        _isPressedQ = true;
                        _isPressed = true;
                        Spell1Press();
                    }
            }
            if (Input.GetKey(KeyCode.Q))
            {
                Spell1Style.normal.background = Spell1DownTexture;
                // if (_isPressedQ && _isPressed)
                // Spell1Down();
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (_isPressedQ && _isPressed)
                {
                    _isPressedQ = false;
                    _isPressed = false;
                    // Spell1Up(); 
                }
                Spell1Style.normal.background = Spell1Texture;
            }
            #endregion

            #region ButtonW
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!_isPressed)
                    if (Time.time > cooldown2 + coolTime2)
                    {
                        _isPressedW = true;
                        _isPressed = true;
                        Spell2Press();
                    }
            }
            if (Input.GetKey(KeyCode.W))
            {
                Spell2Style.normal.background = Spell2DownTexture;
                if (_isPressedW && _isPressed)
                    Spell2Down();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (_isPressedW && _isPressed)
                {
                    _isPressedW = false;
                    _isPressed = false;
                    Spell2Up();
                }
                Spell2Style.normal.background = Spell2Texture;
            }
            #endregion

            #region ButtonE
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_isPressed)
                    if (Time.time > cooldown3 + coolTime3)
                    {
                        _isPressedE = true;
                        _isPressed = true;
                        Spell3Press();
                    }
            }
            if (Input.GetKey(KeyCode.E))
            {
                Spell3Style.normal.background = Spell3DownTexture;
                // if (_isPressedE && _isPressed)
                //  Spell3Down();   
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                if (_isPressedE && _isPressed)
                {
                    _isPressedE = false;
                    _isPressed = false;
                    // Spell3Up();
                }
                Spell3Style.normal.background = Spell3Texture;
            }
            #endregion

            #region ButtonR

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!_isPressed)
                {
                    if (Time.time > cooldown4 + coolTime4)
                    {
                        _isPressedR = true;
                        _isPressed = true;
                        Spell4Press();
                    }
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                Spell4Style.normal.background = Spell4DownTexture;
                //  if (_isPressedR && _isPressed)
                //  Spell4Down();
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                if (_isPressedR && _isPressed)
                {
                    _isPressed = false;
                    _isPressed = false;
                    // Spell4Up();
                }
                Spell4Style.normal.background = Spell4Texture;
            }
            #endregion


        }
    }

    #region SpellController

    public void Spell1Press()
    {
        Invoke(spell1PressName, 0f);
    }
    public void Spell1Down()
    {
        // Invoke(spell1DownName, 0f);
    }
    public void Spell1Up()
    {
        // Invoke(spell1UpName, 0f);
    }
    public void Spell2Press()
    {
        Invoke(spell2PressName, 0f);
    }
    public void Spell2Down()
    {
        Invoke(spell2DownName, 0f);
    }
    public void Spell2Up()
    {
        Invoke(spell2UpName, 0f);
    }
    public void Spell3Press()
    {
        Invoke(spell3PressName, 0f);
    }
    public void Spell3Down()
    {
        // Invoke(spell3DownName, 0f);
    }
    public void Spell3Up()
    {
        // Invoke(spell3UpName, 0f);
    }
    public void Spell4Press()
    {
        Invoke(spell4PressName, 0f);
    }
    public void Spell4Down()
    {
        // Invoke(spell4DownName, 0f);
    }
    public void Spell4Up()
    {
        //Invoke(spell4UpName, 0f);
    }
    public void SpellLCPress()
    {
        Invoke(spellLCPressName, 0f);
    }
    public void SpellLCDown()
    {
        Invoke(spellLCDownName, 0f);
    }
    public void SpellLCUp()
    {
        Invoke(spellLCUpName, 0f);
    }
    public void SpellRCPress()
    {
        Invoke(spellRCPressName, 0f);
    }
    public void SpellRCDown()
    {
        Invoke(spellRCDownName, 0f);
    }
    public void SpellRCUp()
    {
        Invoke(spellRCUpName, 0f);
    }

    #endregion

    #region MageSpells

    //Teleport Spell
    public void MageSpell1Press()
    {

        if (GetComponent<Spells>().portalCreated)
        {
            if (GetComponent<Combat>().mana - 15 >= 0)
            {
                GetComponent<Spells>().CmdPortalTeleport();
                coolTime1 = Time.time;
                GetComponent<Combat>().CmdManaUse(15);
                Spell1Texture = GetComponent<SpellTextures>().MageSpell1;
                Spell1DownTexture = GetComponent<SpellTextures>().MageSpell1Pressed;
                Spell1Style.normal.background = Spell1Texture;

            }
        }
        else if (GetComponent<Combat>().mana - 30 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(30);
            GetComponent<Spells>().CmdCreatePortal();
            Spell1Texture=GetComponent<SpellTextures>().MageSpell1Teleport;
            Spell1DownTexture= GetComponent<SpellTextures>().MageSpell1TeleportPressed;
            Spell1Style.normal.background = Spell1Texture;
        }
    }
    public void MageSpell1Down()
    {

    }
    public void MageSpell1Up()
    {

    }
    //Frost Nova
    public void MageSpell2Press()
    {
        timeButtonPressed = Time.time;
    }
    public void MageSpell2Down()
    {

    }
    public void MageSpell2Up()
    {
        timeButtonPressLast = Time.time - timeButtonPressed;
        if (GetComponent<Combat>().mana - 25 >= 0)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Spells>().CmdGranadeFrostNova(mousePosition, this.transform.position, timeButtonPressLast);
            coolTime2 = Time.time;
            GetComponent<Combat>().CmdManaUse(25);
        }
    }
    //Invisible Spell
    public void MageSpell3Press()
    {
        if (GetComponent<Combat>().mana - 35 >= 0)
        {
            GetComponent<Spells>().CmdInvisibleStart();
            GetComponent<Spells>().Invoke("CmdInvisibleEnd", 5f);
            coolTime3 = Time.time;
            GetComponent<Combat>().CmdManaUse(35);
        }
    }
    public void MageSpell3Down()
    {

    }
    public void MageSpell3Up()
    {

    }
    //Heal
    public void MageSpell4Press()
    {
        GetComponent<Combat>().CmdHealMe(GetComponent<Combat>().Maxhealth / 2);
        coolTime4 = Time.time;
    }
    public void MageSpell4Down()
    {

    }
    public void MageSpell4Up()
    {

    }
    //Laser Spell
    public void MageSpellLCPress()
    {
        GetComponent<Spells>().CmdLaser(new Vector3(this.transform.position.x + tmpx / 1.2f, this.transform.position.y + tmpy / 1.2f, 0), ang, tmpY);
    }
    public void MageSpellLCDown()
    {
        if (tmpSpell2 != null)
        {
            tmpSpell2.transform.position = new Vector3(this.transform.position.x + tmpx / 1.2f, this.transform.position.y + tmpy / 1.2f, 0);
            tmpSpell2.transform.rotation = Quaternion.Euler(ang, tmpY * 90, 0);
            if (GetComponent<Combat>().mana - 1 > 0)
                GetComponent<Combat>().CmdManaUse(1);
            else
                tmpSpell2.GetComponent<Laser>().CmdStopEmission();
        }
    }
    public void MageSpellLCUp()
    {
        if (tmpSpell2 != null)
            tmpSpell2.GetComponent<Laser>().CmdDestroyMe(4);
        tmpSpell2.GetComponent<Laser>().CmdStopEmission();
    }
    //Mage Shield
    public void MageSpellRCPress()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Spells>().CmdMagicShield(mousePosition, this.transform.position, 1);
    }
    public void MageSpellRCDown()
    {
        if (tmpSpell1 != null)
        {
            tmpSpell1.transform.position = new Vector3(this.transform.position.x + tmpx * 1.5f, this.transform.position.y + tmpy * 1.5f, 0);
            if (velocity.x > 0)
                degrees = 180;
            tmpSpell1.transform.rotation = Quaternion.Euler(0, degrees, ang);
            if (GetComponent<Combat>().mana - 1 > 0)
                GetComponent<Combat>().CmdManaUse(1);
            else
                tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
        }
    }
    public void MageSpellRCUp()
    {
        if (tmpSpell1 != null)
            tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
    }

    #endregion

    #region WarlockSpells
    //Clone Spell
    public void WarlockSpell1Press()
    {
        if (GetComponent<Combat>().mana - 30 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(30);
            GetComponent<Spells>().CmdMakeClones();
            GetComponent<Spells>().Invoke("CmdCloneEnds", 5f);
            coolTime1 = Time.time;
        }
    }
    public void WarlockSpell1Down()
    {

    }
    public void WarlockSpell1Up()
    {

    }

    //Poison Bomb
    public void WarlockSpell2Press()
    {
        timeButtonPressed = Time.time;
    }
    public void WarlockSpell2Down()
    {

    }
    public void WarlockSpell2Up()
    {
        timeButtonPressLast = Time.time - timeButtonPressed;

        if (GetComponent<Combat>().mana - 25 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(25);
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Spells>().CmdGranadePoison(mousePosition, this.transform.position, timeButtonPressLast);
            coolTime2 = Time.time;
        }
    }

    //Small hero
    public void WarlockSpell3Press()
    {
        if (GetComponent<Combat>().mana - 30 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(30);
            GetComponent<Spells>().CmdGetSmall();
            GetComponent<Spells>().Invoke("CmdGetBig", 7f);
            coolTime3 = Time.time;
        }
    }
    public void WarlockSpell3Down()
    {

    }
    public void WarlockSpell3Up()
    {

    }
    //Heal
    public void WarlockSpell4Press()
    {
        GetComponent<Combat>().CmdHealMe(GetComponent<Combat>().Maxhealth / 2);
        coolTime4 = Time.time;
    }
    public void WarlockSpell4Down()
    {

    }
    public void WarlockSpell4Up()
    {

    }
    //PoisonDart
    public void WarlockSpellLCPress()
    {
        if (GetComponent<Combat>().mana - 3 >= 0)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Spells>().CmdPoisonDart(mousePosition, this.transform.position);
            GetComponent<Combat>().CmdManaUse(3);

        }
    }
    public void WarlockSpellLCDown()
    {

    }
    public void WarlockSpellLCUp()
    {

    }
    //Warlock Shield
    public void WarlockSpellRCPress()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Spells>().CmdMagicShield(mousePosition, this.transform.position, 2);
    }
    public void WarlockSpellRCDown()
    {
        if (tmpSpell1 != null)
        {
            tmpSpell1.transform.position = new Vector3(this.transform.position.x + tmpx * 1.5f, this.transform.position.y + tmpy * 1.5f, 0);
            if (velocity.x > 0)
                degrees = 180;
            tmpSpell1.transform.rotation = Quaternion.Euler(0, degrees, ang);
            if (GetComponent<Combat>().mana - 1 > 0)
                GetComponent<Combat>().CmdManaUse(1);
            else
                tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
        }
    }
    public void WarlockSpellRCUp()
    {
        if (tmpSpell1 != null)
            tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
    }

    #endregion

    #region FiremanSpells

    //Fire Rocket Jump
    public void FiremanSpell1Press()
    {
        if (GetComponent<Combat>().mana - 20 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(20);
            GetComponent<Spells>().CmdFireTrail();
            coolTime1 = Time.time;
        }
    }
    public void FiremanSpell1Down()
    {

    }
    public void FiremanSpell1Up()
    {

    }
    //Fire Breath
    public void FiremanSpell2Press()
    {
        if (GetComponent<Combat>().mana - 30 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(30);
            GetComponent<Spells>().CmdFireParticleSpell(new Vector3(this.transform.position.x + tmpx * 2, this.transform.position.y + tmpy * 2, 0), ang, tmpY);
            coolTime2 = Time.time;
        }
    }
    public void FiremanSpell2Down()
    {
        if (tmpSpell2 != null)
        {
            tmpSpell2.transform.position = new Vector3(this.transform.position.x + tmpx * 2, this.transform.position.y + tmpy * 2, 0);
            tmpSpell2.transform.rotation = Quaternion.Euler(ang, tmpY * 90, 0);
        }
    }
    public void FiremanSpell2Up()
    {
        if (tmpSpell2 != null)
            tmpSpell2.GetComponent<FireSpell>().CmdDestroyMe(4);
    }
    //Fire Transform
    public void FiremanSpell3Press()
    {
        if (GetComponent<Combat>().mana - 35 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(35);
            GetComponent<Spells>().FiremanTransformStart(3);
            coolTime3 = Time.time;
        }
    }
    public void FiremanSpell3Down()
    {

    }
    public void FiremanSpell3Up()
    {

    }
    //Heal
    public void FiremanSpell4Press()
    {
        GetComponent<Combat>().CmdHealMe(GetComponent<Combat>().Maxhealth / 2);
        coolTime4 = Time.time;
    }
    public void FiremanSpell4Down()
    {

    }
    public void FiremanSpell4Up()
    {

    }
    //Fireball
    public void FiremanSpellLCPress()
    {
        if (GetComponent<Combat>().mana - 4 >= 0)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Spells>().CmdFireball(mousePosition, this.transform.position);
            GetComponent<Combat>().CmdManaUse(4f);
        }
    }
    public void FiremanSpellLCDown()
    {

    }
    public void FiremanSpellLCUp()
    {

    }
    //Fireman Shield
    public void FiremanSpellRCPress()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Spells>().CmdMagicShield(mousePosition, this.transform.position, 3);
    }
    public void FiremanSpellRCDown()
    {
        if (tmpSpell1 != null)
        {
            tmpSpell1.transform.position = new Vector3(this.transform.position.x + tmpx * 1.5f, this.transform.position.y + tmpy * 1.5f, 0);
            if (velocity.x > 0)
                degrees = 180;
            tmpSpell1.transform.rotation = Quaternion.Euler(0, degrees, ang);
            if (GetComponent<Combat>().mana - 1 > 0)
                GetComponent<Combat>().CmdManaUse(1);
            else
                tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
        }
    }
    public void FiremanSpellRCUp()
    {
        if (tmpSpell1 != null)
            tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
    }
    #endregion

    #region ClericSpells
    //Speed Up
    public void ClericSpell1Press()
    {
        if (GetComponent<Combat>().mana - 20 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(20);
            GetComponent<Spells>().SpeedUpStart();
            coolTime1 = Time.time;
        }
    }
    public void ClericSpell1Down()
    {

    }
    public void ClericSpell1Up()
    {

    }
    //Holy Explosion
    public void ClericSpell2Press()
    {
        if (GetComponent<Combat>().mana - 25 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(25);
            GetComponent<Spells>().CmdHolyExplosionStart(mousePosition);
            coolTime2 = Time.time;
        }
    }
    public void ClericSpell2Down()
    {

    }
    public void ClericSpell2Up()
    {

    }
    //Bubble Spell
    public void ClericSpell3Press()
    {
        if (GetComponent<Combat>().mana - 35 >= 0)
        {
            GetComponent<Combat>().CmdManaUse(35);
            GetComponent<Spells>().CmdBubble();
            coolTime3 = Time.time;
        }
    }
    public void ClericSpell3Down()
    {

    }
    public void ClericSpell3Up()
    {

    }
    //Heal
    public void ClericSpell4Press()
    {
        GetComponent<Combat>().CmdHealMe(GetComponent<Combat>().Maxhealth / 2);
        coolTime4 = Time.time;
    }
    public void ClericSpell4Down()
    {

    }
    public void ClericSpell4Up()
    {

    }
    //HolyLight
    public void ClericSpellLCPress()
    {
        GetComponent<Spells>().CmdHolyLight(new Vector3(this.transform.position.x + tmpx / 1.2f, this.transform.position.y + tmpy / 1.2f, 0), ang, tmpY);
    }
    public void ClericSpellLCDown()
    {
        if (tmpSpell2 != null)
        {
            tmpSpell2.transform.position = new Vector3(this.transform.position.x + tmpx / 1.2f, this.transform.position.y + tmpy / 1.2f, 0);
            tmpSpell2.transform.rotation = Quaternion.Euler(ang, tmpY * 90, 0);
            if (GetComponent<Combat>().mana - 1 > 0)
                GetComponent<Combat>().CmdManaUse(1);
            else
                tmpSpell2.GetComponent<Laser>().CmdStopEmission();
        }
    }
    public void ClericSpellLCUp()
    {
        if (tmpSpell2 != null)
            tmpSpell2.GetComponent<Laser>().CmdDestroyMe(4);
        tmpSpell2.GetComponent<Laser>().CmdStopEmission();
    }
    //Cleric Shield
    public void ClericSpellRCPress()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Spells>().CmdMagicShield(mousePosition, this.transform.position, 4);
    }
    public void ClericSpellRCDown()
    {
        if (tmpSpell1 != null)
        {
            tmpSpell1.transform.position = new Vector3(this.transform.position.x + tmpx * 1.5f, this.transform.position.y + tmpy * 1.5f, 0);
            if (velocity.x > 0)
                degrees = 180;
            tmpSpell1.transform.rotation = Quaternion.Euler(0, degrees, ang);
            if (GetComponent<Combat>().mana - 1 > 0)
                GetComponent<Combat>().CmdManaUse(1);
            else
                tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
        }
    }
    public void ClericSpellRCUp()
    {
        if (tmpSpell1 != null)
            tmpSpell1.GetComponent<MagicShield>().CmdDestroyMe();
    }

    #endregion

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            /// ////////////////////////
            // GUI.BeginGroup(new Rect(200, 120, 100, 100));
            /*  if(progTex!=null)
                  GUI.DrawTexture(new Rect(210, 120, 40, 40), progTex);*/
            //  GUI.EndGroup();
            xoffset = Screen.width - 234;
            yoffset = Screen.height - 70;
            GUI.skin.font = myFont;


            GUI.DrawTexture(new Rect(xoffset / 2 - 40, yoffset - 75, 330, 145), ActionBarTexture);

            GUI.BeginGroup(new Rect(xoffset / 2 - 20, yoffset + 20, 300, 70));

            GUI.DrawTexture(new Rect(1, 0, 40, 40), SpellLockedTexture);
            GUI.DrawTexture(new Rect(52, 0, 40, 40), SpellLockedTexture);
            GUI.DrawTexture(new Rect(102, 0, 40, 40), SpellLockedTexture);

            GUI.DrawTexture(new Rect(199, 0, 40, 40), SpellLockedTexture);
            GUI.DrawTexture(new Rect(249, 0, 40, 40), SpellLockedTexture);

            //Spell Icons
            if (GUI.Button(new Rect(1, 0, 40, 40), SpellEmptyTexture, Spell1Style))
            {

            }
            if (GUI.Button(new Rect(52, 0, 40, 40), SpellEmptyTexture, Spell2Style))
            {

            }

            if (GUI.Button(new Rect(102, 0, 40, 40), SpellEmptyTexture, Spell3Style))
            {

            }

            if (GUI.Button(new Rect(153, 0, 40, 40), SpellEmptyTexture, Spell4Style))
            {

            }
            if (GUI.Button(new Rect(204, 0, 40, 40), SpellEmptyTexture, Spell5Style))
            {

            }
            if (GUI.Button(new Rect(255, 0, 40, 40), SpellEmptyTexture, Spell6Style))
            {

            }

            //Text
            GUI.Label(new Rect(17, 33, 50, 50), "Q");
            GUI.Label(new Rect(67, 33, 50, 50), "W");
            GUI.Label(new Rect(117, 33, 50, 50), "E");
            GUI.Label(new Rect(162, 33, 50, 50), "R");
            GUI.Label(new Rect(210, 33, 50, 50), "LC");
            GUI.Label(new Rect(260, 33, 50, 50), "RC");

            GUI.EndGroup();

            //Cooldown effect
            if (cooldown1 - Time.time + coolTime1 > 0)
            {
                GUI.DrawTexture(new Rect(xoffset / 2 - 20 + 1, 20 + yoffset, 40, ((cooldown1 - (Time.time - coolTime1))) * 40 / cooldown1), cooldownTexture);
                GUI.Label(new Rect(xoffset / 2, -10 + yoffset, 30, 30), ((int)(cooldown1 - Time.time + coolTime1)).ToString());
                // progress =( ((cooldown1 - (Time.time - coolTime1))) * 40 / cooldown1);
            }
            if (cooldown2 - Time.time + coolTime2 > 0)
            {
                GUI.DrawTexture(new Rect(xoffset / 2 - 20 + 51, 20 + yoffset, 40, ((cooldown2 - (Time.time - coolTime2))) * 40 / cooldown2), cooldownTexture);
                GUI.Label(new Rect(xoffset / 2 + 50, -10 + yoffset, 30, 30), ((int)(cooldown2 - Time.time + coolTime2)).ToString());
            }
            if (cooldown3 - Time.time + coolTime3 > 0)
            {
                GUI.DrawTexture(new Rect(xoffset / 2 - 20 + 101, 20 + yoffset, 40, ((cooldown3 - (Time.time - coolTime3))) * 40 / cooldown3), cooldownTexture);
                GUI.Label(new Rect(xoffset / 2 + 100, -10 + yoffset, 30, 30), ((int)(cooldown3 - Time.time + coolTime3)).ToString());
            }
            if (cooldown4 - Time.time + coolTime4 > 0)
            {
                GUI.DrawTexture(new Rect(xoffset / 2 - 20 + 151, 20 + yoffset, 40, ((cooldown4 - (Time.time - coolTime4))) * 40 / cooldown4), cooldownTexture);
                GUI.Label(new Rect(xoffset / 2 + 150, -10 + yoffset, 30, 30), ((int)(cooldown4 - Time.time + coolTime4)).ToString());
            }
            //No cooldowns on left and right click
            /*  if (cooldownLC - Time.time + coolTimeLC > 0)
              {
                  GUI.DrawTexture(new Rect(xoffset / 2 - 20 + 201, 20 + yoffset, 40, ((cooldownLC - (Time.time - coolTimeLC))) * 40 / cooldownLC), cooldownTexture);
                  GUI.Label(new Rect(xoffset / 2 + 200, -10 + yoffset, 30, 30), ((int)(cooldownLC - Time.time + coolTimeLC)).ToString());
              }
              if (cooldownRC - Time.time + coolTimeRC > 0)
              {
                  GUI.DrawTexture(new Rect(xoffset / 2 - 20 + 251, 20 + yoffset, 40, ((cooldownRC - (Time.time - coolTimeRC))) * 40 / cooldownRC), cooldownTexture);
                  GUI.Label(new Rect(xoffset / 2 + 250, -10 + yoffset, 30, 30), ((int)(cooldownRC - Time.time + coolTimeRC)).ToString());
              }*/

        }
    }

    /* public Texture2D ProgressUpdate(float progress,Color overlayColor)
     {
         var thisTex = new Texture2D(cooldownTexture.width, cooldownTexture.height);
         var centre =new Vector2(Mathf.Ceil(thisTex.width / 2), Mathf.Ceil(thisTex.height / 2)); //find the centre pixel
         for(int y=0; y < thisTex.height; y++){
             for (int x=0; x < thisTex.width; x++){
                 var angle = Mathf.Atan2(x - centre.x, y - centre.y) * Mathf.Rad2Deg; //find the angle between the centre and this pixel (between -180 and 180)
                 if (angle < 0)
                 {
                     angle += 360; //change angles to go from 0 to 360
                 }
                 var pixColor = cooldownTexture.GetPixel(x, y);
                 if (angle <= progress * 360.0)
                 { //if the angle is less than the progress angle blend the overlay colour
                     pixColor =new Color(
                         (pixColor.r * pixColor.a * (1 - overlayColor.a)) + (overlayColor.r * overlayColor.a),
                         (pixColor.g * pixColor.a * (1 - overlayColor.a)) + (overlayColor.g * overlayColor.a),
                         (pixColor.b * pixColor.a * (1 - overlayColor.a)) + (overlayColor.b * overlayColor.a)
                                     );
                     thisTex.SetPixel(x, y, pixColor);
                 }
                 else
                 {
                     thisTex.SetPixel(x, y, pixColor);
                 }
             }
         }
         thisTex.Apply(); //apply the cahnges we made to the texture
         return thisTex;
     }*/

}