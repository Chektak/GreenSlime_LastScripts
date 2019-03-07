using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUpdate : MonoBehaviour {
    public Text goldCoinText;
    public Text slimeCoinText;

    private void Update()
    {
        goldCoinText.text = ": "+GameManager.Instance.goldCoin.ToString();
        slimeCoinText.text = ": "+GameManager.Instance.slimeCoin.ToString();
    }
}
