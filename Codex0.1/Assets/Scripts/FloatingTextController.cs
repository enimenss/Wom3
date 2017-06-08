using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FloatingTextController : NetworkBehaviour
{

    private static FloatingText popupRedText;
    private static FloatingText popupGreenText;
    private static FloatingText popupBlueText;
     private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupRedText)
            popupRedText = Resources.Load<FloatingText>("PopupTextParent");
        if (!popupGreenText)
            popupGreenText = Resources.Load<FloatingText>("PopupTextParentGreen");
        if (!popupBlueText)
            popupBlueText = Resources.Load<FloatingText>("PopupTextParentBlue");
          if (!popupText)
              popupText = Resources.Load<FloatingText>("PopupTextParentWord");

    }
      public static void CreateFloatingText(string text, Transform location,float x,float y)
      {
          if (popupText != null)
          {
              FloatingText instance = Instantiate(popupText);
              Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y  /*+Random.Range(-.5f, .5f)*/ ));
              screenPosition.x -= x;
              screenPosition.y -= y;
              instance.transform.SetParent(canvas.transform, false);
              instance.transform.position = screenPosition;
              instance.SetText(text);


          }
      }
    //2sec
    public static void CreateRedFloatingText(string text, Transform location)
    {
        if (popupRedText != null)
        {
            FloatingText instance = Instantiate(popupRedText);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.5f, .5f), location.position.y + Random.Range(-.5f, .5f)));
         //  Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(this.transform.position.x + Random.Range(-.5f, .5f), this.transform.position.y + Random.Range(-.5f, .5f)));
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;
            instance.SetText(text);

        }
    }
    public static void CreateGreenFloatingText(string text, Transform location, float x, float y)
    {
        if (popupGreenText != null)
        {
            FloatingText instance = Instantiate(popupGreenText);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.3f, .4f), location.position.y + Random.Range(-.25f, .0f)));
            screenPosition.x -= x;
            screenPosition.y -= y;
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;
            instance.SetText(text);

        }
    }
    public static void CreateBlueFloatingText(string text, Transform location, float x, float y)
    {
        if (popupBlueText != null)
        {
            FloatingText instance = Instantiate(popupBlueText);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.4f, .3f), location.position.y + Random.Range(-.25f, 0f)));
            screenPosition.x += x;
            screenPosition.y -= y;
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;
            instance.SetText(text);

        }
    }

}
