using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageClear : MonoBehaviour {
    public int outsidestageNum;
    public int insideStageNum;
    public int nextOutsideStageNum;
    public int nextInsideStageNum;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (outsidestageNum != 0 && insideStageNum != 0)
            {//스테이지 번호가 제대로 설정되었다면
                GameManager.Instance.AddSlimeCoin(outsidestageNum, insideStageNum);//슬라임코인을 더한다.
                GameManager.Instance.StageLockRelease(nextOutsideStageNum, nextInsideStageNum);
            }
            SuperManager superManager;
            GameManager.Instance.managers.TryGetValue("GUIManager", out superManager);//GameManager에서 GUI매니저에 대한 정보를 얻는다.
            GUIManager guiManager = superManager.gameObject.GetComponent<GUIManager>();
            guiManager.GetComponent<GUIController>().stageClearPanel.SetActive(true);
            Time.timeScale = 0;
            Destroy(this.gameObject);
        }
    }
}
