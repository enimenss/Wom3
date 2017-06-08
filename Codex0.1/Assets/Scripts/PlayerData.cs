using Assets.Class;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { oneVSone, twoVStwo, threeVSthree, fiveVSfive, Custom };

public class PlayerData : MonoBehaviour
{
    public User LoginUser;
    public UserStats Stats;
    public Token token;
    public List<Assets.Class.Items> ListofItems;
    public GameType Type;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Type = GameType.oneVSone;
    }

}
