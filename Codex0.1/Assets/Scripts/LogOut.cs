using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogOut : MonoBehaviour
{

	// Use this for initialization
    
	void Start ()
    {
        var s = this.GetComponent<Button>();
        s.onClick.AddListener(GameObject.Find("controler").GetComponent<API>().Logout);
	}
	
}
