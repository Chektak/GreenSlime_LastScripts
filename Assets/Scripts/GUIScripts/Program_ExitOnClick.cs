using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program_ExitOnClick : MonoBehaviour {
    public void SaveAndProgramExit()
    {
        GameManager.Instance.SaveBinary(Application.persistentDataPath + "/Mydata.sav");
        Debug.Log(Application.persistentDataPath + "/Mydata.sav");
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
