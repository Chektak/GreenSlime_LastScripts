using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChanger : MonoBehaviour {
    public string sceneName;
    public int stageNum;
    public int insideStageNum;
    public AudioClip stageBGM;

	public void SceneChange()
    {
        SuperManager superManager;
        GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 GUI매니저에 대한 정보를 얻는다.
        SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
        

        if (stageNum == 0 && insideStageNum == 0)
        {//둘다 0일경우 스테이지 이외 씬 이동으로 간주한다.
            SceneManager.LoadScene(sceneName);
            GameManager.Instance.nowPlayingScnenName = sceneName;
            soundManager.ChangeBGM(stageBGM);//인스펙터에 넣은 브금으로 바꾼다.
            return;
        }
        if (GameManager.Instance.isStageLocked[stageNum][insideStageNum] == false)
        {
            SceneManager.LoadScene(sceneName);
            GameManager.Instance.nowPlayingScnenName = sceneName;
            soundManager.ChangeBGM(stageBGM);//인스펙터에 넣은 브금으로 바꾼다.
        }
    }

    private void OnEnable()
    {
       if (stageNum == 0 && insideStageNum == 0)
            return;
        //Invoke("updateButtonSprite", 0.01f);*/
        if (GameManager.Instance.isStageLocked[stageNum][insideStageNum] == false)
        {
            SuperManager superManager;
            GameManager.Instance.managers.TryGetValue("GUIManager", out superManager);//GameManager에서 GUI매니저에 대한 정보를 얻는다.
            GUIManager guiManager = superManager.gameObject.GetComponent<GUIManager>();
            GetComponent<Image>().sprite = guiManager.insideStageLockOffSprites[insideStageNum - 1];
        }
    }

    void updateButtonSprite()
    {
        if (GameManager.Instance.isStageLocked[stageNum][insideStageNum] == false)
        {
            SuperManager superManager;
            GameManager.Instance.managers.TryGetValue("GUIManager", out superManager);//GameManager에서 GUI매니저에 대한 정보를 얻는다.
            GUIManager guiManager = superManager.gameObject.GetComponent<GUIManager>();
            GetComponent<Image>().sprite = guiManager.insideStageLockOffSprites[insideStageNum - 1];
        }
    }
}
