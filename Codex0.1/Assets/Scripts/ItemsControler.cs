using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsControler : MonoBehaviour
{
    public GameObject Text;

    public float Xofset;
    public float Yofset;
    public int NumbofRows;
    public float startPosX;
    public float startPosY;
    private PlayerData Data;

    public GameObject item;
    private int page = 0;
    public RectTransform[] lista;
    public int maxElements;
    public Text PageNum;

    public ItemControler selectedItem;





    public void OnEnable()
    {
        page = 0;
        pupulate();
        PageNum.text = page.ToString();
    }

    public void pupulate()
    {
        Debug.Log("start1");
        Data = GameObject.Find("controler").GetComponent<PlayerData>();
        if (page > Data.ListofItems.Count)
        {
            page--;
            pupulate();
        }
        Debug.Log(page);
        foreach (Transform t in lista[0])
            Destroy(t.gameObject);
        foreach (Transform t in lista[1])
            Destroy(t.gameObject);
        foreach (Transform t in lista[2])
            Destroy(t.gameObject);
        for (int i = 15 * page, j = 0, index = 0; i < Data.ListofItems.Count; i++, index++)
        {
            if (index == 15)
                break;
            GameObject s = Instantiate(item) as GameObject;
            s.GetComponent<ItemControler>().populate(Data.ListofItems[i], this);
            s.transform.SetParent(lista[j], false);
            Debug.Log(j);
            Debug.Log(index);
            Debug.Log(i);
            if (j == 0)
            {
                if ((j + 1) * 4 <= index)
                    j++;
            }
            else if ((j + 1) * 5 <= index + 1)
            {
                j++;
            }

        }
    }
    public void PageUp()
    {
        page++;
        pupulate();
        PageNum.text = page.ToString();
    }
    public void PageDown()
    {
        if (page > 0)
        {
            page--;
            pupulate();
        }
        PageNum.text = page.ToString();

    }

    public void selectItem(ItemControler item)
    {
        if (selectedItem != null)
            selectedItem.outline.SetActive(false);
        selectedItem = item;
        selectedItem.outline.SetActive(true);
    }
}
