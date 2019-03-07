using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SuperManager {
    //씬이 바뀔때마다 바뀌는 배경음악을 재생하고, UI 버튼을 클릭할때마다 버튼소리를 재생하는 책임을 갖는 스크립트이다.
    public AudioClip mainBGM;

    public AudioSource bgmAudioSource;
    public AudioSource soundEffectSource;

    protected void Start()
    {
        bgmAudioSource.clip=mainBGM;
        bgmAudioSource.Play();
    }

    /// <summary>
    /// 인잣값으로 넣은 BGM으로 현재 재생중인 음악을 바꾼다.
    /// </summary>
    /// <param name="valueBGM"></param>
    public void ChangeBGM(AudioClip valueBGM)
    {
        bgmAudioSource.clip = valueBGM;
        bgmAudioSource.Play();
    }

    /// <summary>
    /// 버튼을 클릭할때 나오는 효과음을 재생하는 함수이다.
    /// </summary>
    public void PlaySoundEffect()
    {
        soundEffectSource.Play();
    }
}
