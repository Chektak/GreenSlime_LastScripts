using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop_ItemInfoPanelSet : MonoBehaviour {
    //상점에서 아이템이미지가 클릭될시 나타나는 패널에 나타나는 정보를 자동화시켜주는 책임을 가지는 스크립트이다.
    public Text panelItemName;
    public Image panelItemImage;
    public Text panelItemTipText;
    public Text panelItemSpecialText;
    public Text panelItemPriceText;
    [HideInInspector]
    public string itemName;
    [HideInInspector]
    public string itemTip;
    [HideInInspector]
    public Sprite itemSprite;
    [HideInInspector]
    public string itemSpecialText;
    [HideInInspector]
    public string itemPriceText;
    private void OnEnable()
    {
        StartCoroutine(RefreshInfo());
            
    }
    IEnumerator RefreshInfo()
    {
        yield return null;//정보를 받기위해 1프레임 기다린다.
        panelItemName.text = itemName;
        panelItemImage.sprite = itemSprite;
        panelItemTipText.text = itemTip;
        panelItemSpecialText.text = itemSpecialText;
        panelItemPriceText.text = itemPriceText;
        yield break;
    }
}
