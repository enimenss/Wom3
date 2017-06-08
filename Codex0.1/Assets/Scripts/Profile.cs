using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Text Username;
    public Text Email;
    public Text Win;
    public Text Loses;
    public Text Total;
    public Text Poents;
    public Text Gold;


    private PlayerData Data;

    void Start ()
    {
        Debug.Log("start1");
        Data = GameObject.Find("controler").GetComponent<PlayerData>();
        Username.text = "Username:" + Data.LoginUser.Username;
        Email.text = "Email:" + Data.LoginUser.Email;
        Win.text = "Wins:" + Data.Stats.Wins.ToString();
        Loses.text = "Loses:" + Data.Stats.Loses.ToString();
        Total.text = "Total:" + Data.Stats.Total.ToString();
        Poents.text = "Points:" + Data.Stats.Points.ToString();
        Gold.text = "Gold:" + Data.Stats.Gold.ToString();
    }

    
    public void OnEnable()
    {
        Debug.Log("start2");
        if (Data.LoginUser.Username == null)
            return;
        Username.text = "Username:" + Data.LoginUser.Username;
        Email.text = "Email:" + Data.LoginUser.Email;
        Win.text = "Wins:" + Data.Stats.Wins.ToString();
        Loses.text = "Loses:" + Data.Stats.Loses.ToString();
        Total.text = "Total:" + Data.Stats.Total.ToString();
        Poents.text = "Points:" + Data.Stats.Points.ToString();
        Gold.text = "Gold:" + Data.Stats.Gold.ToString();
    }
    public void UpdateData()
    {
        Username.text = "Username:" + Data.LoginUser.Username;
        Email.text = "Email:" + Data.LoginUser.Email;
        Win.text = "Wins:" + Data.Stats.Wins.ToString();
        Loses.text = "Loses:" + Data.Stats.Loses.ToString();
        Total.text = "Total:" + Data.Stats.Total.ToString();
        Poents.text = "Points:" + Data.Stats.Points.ToString();
        Gold.text = "Gold:" + Data.Stats.Gold.ToString();
    }
}
