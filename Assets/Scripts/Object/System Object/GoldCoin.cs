using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour {
    //골드코인에 붙을 스크립트이다.

    public int coinValue = 10;
    public AudioClip destroySoundEffect;

	void Update () {
        transform.Rotate(0, 180* Time.deltaTime, 0);
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SuperManager superManager;
            GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 사운드매니저에 대한 정보를 얻는다.
            SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
            soundManager.soundEffectSource.PlayOneShot(destroySoundEffect);
            GameManager.Instance.AddGoldCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
