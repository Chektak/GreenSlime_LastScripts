using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_ItemBuyOnClick : MonoBehaviour {
    public GameObject item;
    public int itemPriceGoldCoin=0;//아이템의 가격(골드)
    public int itemPriceSlimeCoin = 0;//아이템의 가격(슬라임)

    public void BuyOnClick()
    {
        if (GameManager.Instance.goldCoin - itemPriceGoldCoin < 0 ||
            GameManager.Instance.slimeCoin - itemPriceSlimeCoin < 0)
            return;
        GameManager.Instance.goldCoin -= itemPriceGoldCoin;
        GameManager.Instance.slimeCoin -= itemPriceSlimeCoin;
        GameManager.Instance.buyItemKeys.Add(item.GetComponent<Item>().itemKey);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Item itemInfo = item.GetComponent<Item>();
        foreach (int code in GameManager.Instance.buyItemKeys)
        {
            if (code == itemInfo.itemKey)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
