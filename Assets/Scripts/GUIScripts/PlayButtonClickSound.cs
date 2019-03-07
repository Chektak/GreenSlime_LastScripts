using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonClickSound : MonoBehaviour {
    //모든 UI버튼에 붙는 스크립트이다. 
    public void PlaySoundEffect()
    {
        SuperManager superManager;
        GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 GUI매니저에 대한 정보를 얻는다.
        SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
        soundManager.PlaySoundEffect();//버튼클릭 효과음을 재생한다.
    }
}
