using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    [SerializeField] public Item item;

    public Image itemImage;
    public Text itemCount;
    RectTransform tr;

    public CanvasGroup E;

    public bool isEquip = false;

    void Start()
    {
        tr = transform.GetComponent<RectTransform>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemCount = GetComponentInChildren<Text>();
        Transform t = transform.Find("E");

        if (t != null)
        {
            E = t.GetComponent<CanvasGroup>();
        }

        if (E != null)
        {
            E.alpha = 0;
        }

        itemCount.raycastTarget = false;
        itemImage.raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        SetSolt();
    }

    void SetSolt()
    {

        if (item == null)
        {
            itemImage.color = Color.clear;
            itemCount.text = "";
            return;
        }


        if (item.itemType == Item.ItemType.Bullet)
        {
            itemCount.text = $"{Managers._charInfo.curAmmo + Managers._charInfo.curFullAmmo}";
        }
        else
        {
            itemCount.text = "";
        }

        itemImage.sprite = Resources.Load<Sprite>("ItemSprite/" + $"{item.ItemName}");
        itemImage.color = new Color(1, 1, 1, 1);
        int temp = 1;

        if (itemImage.sprite.rect.size.x > tr.sizeDelta.x && itemImage.sprite.rect.size.y > tr.sizeDelta.y)
        {
            for (int i = 1; i < 1000; i++)
            {
                if (itemImage.sprite.rect.size.x / temp > tr.sizeDelta.x || itemImage.sprite.rect.size.y / temp > tr.sizeDelta.y)
                {
                    temp++;
                }
                else
                    break;
            }

            itemImage.rectTransform.sizeDelta = new Vector2(itemImage.sprite.rect.size.x / temp, itemImage.sprite.rect.size.y / temp);
        }
        else
        {
            itemImage.rectTransform.sizeDelta = itemImage.sprite.rect.size;

        }

    }
}
