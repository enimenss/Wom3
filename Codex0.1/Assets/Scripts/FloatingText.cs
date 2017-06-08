using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FloatingText :NetworkBehaviour {

    public Animator animator;
    private Text damageText;
    private int size;
	
    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
       
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
        size = damageText.fontSize;
    }
	
    public void SetText(string text)
    {
        damageText.text = text;
        
    }
    public void SetSize(int i)
    {
        damageText.fontSize = i;
    }
}
