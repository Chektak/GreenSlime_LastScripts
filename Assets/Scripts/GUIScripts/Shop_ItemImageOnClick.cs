using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop_ItemImageOnClick : MonoBehaviour {
    public Shop_ItemInfoPanelSet itemInfo;

    public string itemName;
    public string itemTip;
    public string itemSpecialText;
    public string itemPriceText;

    public void ImageOnClick()
    {
        itemInfo.itemName = itemName;
        itemInfo.itemSprite = gameObject.GetComponent<Image>().sprite;
        itemInfo.itemTip = itemTip;
        itemInfo.itemSpecialText = itemSpecialText;
        itemInfo.itemPriceText = "Required a "+itemPriceText;
    }
}
