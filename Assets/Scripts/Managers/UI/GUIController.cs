using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GUIController: MonoBehaviour {
    public GameObject escPauseUI;
    public GameObject stageClearPanel;
    public GameObject stageClearFailPanel;
    private bool paused = false;
    void Start()
    {
        escPauseUI.SetActive(paused);
    }

    bool CanEscPress()
    {
        if (stageClearFailPanel.activeSelf || stageClearPanel.activeSelf)
            return false;//스테이지 클리어나 클리어실패후 나타나는 창에서는 esc가 안먹히게 한다.
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.nowPlayingScnenName != "MainScene")
        {//메인씬이 아닌곳(스테이지)에서 esc를 누를시
            return true;
        }
        return false;
    }
    void Update()
    {
        if (CanEscPress())
        {
            paused = !paused;
        }
        if (paused)
        {
            escPauseUI.SetActive(true);
        }
        if (!paused) {
            escPauseUI.SetActive(false);
        }
    }

    public void Resume()
    {
        paused = false;

    }

    public void Restart()
    {
        SceneManager.LoadScene(GameManager.Instance.nowPlayingScnenName);
        Time.timeScale = 1;
        paused = false;
    }
    public void MainMenu()
    {
        GetComponent<SceneChanger>().SceneChange();
        paused = false;
        Time.timeScale = 1;
    }
    public void ProgramExit()
    {
        Application.Quit();
    }
}
