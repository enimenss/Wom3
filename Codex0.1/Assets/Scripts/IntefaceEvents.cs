using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;
using UnityEngine.SceneManagement;

public class IntefaceEvents : MonoBehaviour
{
    public GameObject Profile;
    public GameObject Items;
    public GameObject Heros;
    public Dropdown GameTypes;

    private GameObject Curent;

    public API a;
    public PlayerData p;
    public ItemsControler itemPanel;
    public Profile profil;
    public float RefresheRate;
    private float time = 0;

    private void Start()
    {
        Curent = Profile;
        Curent.SetActive(true);
        Items.SetActive(false);
        GameTypes.onValueChanged.AddListener(delegate { onChangeValue(GameTypes); });
        a = GameObject.Find("controler").GetComponent<API>();
        p = GameObject.Find("controler").GetComponent<PlayerData>();
    }
    private void Update()
    {
        if (Time.time > time + RefresheRate)
        {
            a.GetUserStat(null);
            a.GetUserItems(null);
            itemPanel.pupulate();
            profil.UpdateData();
            time = Time.time;
            Debug.Log("refresh");
        }
    }
    public void OnItemClick()
    {
        Curent.SetActive(false);
        Curent = Items;
        Curent.SetActive(true);
    }
    public void OnProfileClick()
    {
        Curent.SetActive(false);
        Curent = Profile;
        Curent.SetActive(true);
    }
    public void OnHerosClick()
    {
        Curent.SetActive(false);
        Curent = Heros;
        Curent.SetActive(true);
    }
    public void OnShopClick()
    {
        System.Diagnostics.Process.Start("http://localhost:5611/Home/GotToShop/" + GameObject.Find("controler").GetComponent<PlayerData>().token.token);
    }
    public void OnNewsClick()
    {
        System.Diagnostics.Process.Start("http://localhost:5611/Home/GotToNews/" + GameObject.Find("controler").GetComponent<PlayerData>().token.token);
    }

    public void onChangeValue(Dropdown d)
    {
      //  Debug.Log(d.options[d.value]);
        if (d.options[d.value].text.Equals("Solo 1VS1"))
            GameObject.Find("controler").GetComponent<PlayerData>().Type = GameType.oneVSone;
        else if (d.options[d.value].text.Equals("Group 2VS2"))
            GameObject.Find("controler").GetComponent<PlayerData>().Type = GameType.twoVStwo;
        else if (d.options[d.value].text.Equals("Group 3VS3"))
            GameObject.Find("controler").GetComponent<PlayerData>().Type = GameType.threeVSthree;
        else if (d.options[d.value].text.Equals("Group 4VS4"))
            GameObject.Find("controler").GetComponent<PlayerData>().Type = GameType.fiveVSfive;
        else if (d.options[d.value].text.Equals("Custom Game"))
            GameObject.Find("controler").GetComponent<PlayerData>().Type = GameType.Custom;

     //   Debug.Log(GameObject.Find("controler").GetComponent<PlayerData>().Type);
    }

    public void OnPlayeGameClivk()
    {
        SceneManager.LoadScene("Lobby");
    }

}
