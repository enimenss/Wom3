using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSpell : MonoBehaviour {

    public Image Slika;
    public Text Ime;
    public void change(string ime)
    {
        gameObject.SetActive(true);
        Debug.Log(ime);
        Slika.sprite= Resources.Load<Sprite>("Image/" + ime) as Sprite;
        Ime.text = ime;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
