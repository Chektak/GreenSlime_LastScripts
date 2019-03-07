using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    //하트오브젝트에 붙을 스크립트이다.
    public int healValue = 1;
    public AudioClip destroySoundEffect;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SuperManager superManager;
            GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 사운드매니저에 대한 정보를 얻는다.
            SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
            soundManager.soundEffectSource.PlayOneShot(destroySoundEffect);
            HealthDisplay playerHealth = coll.gameObject.GetComponent<HealthDisplay>();
            playerHealth.Health += healValue;
            Destroy(gameObject);
        }
    }
}
