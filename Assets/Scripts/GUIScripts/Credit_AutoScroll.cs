using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit_AutoScroll : MonoBehaviour {
    public float interval = 2.5f;

    private bool canPlaying = true;
    private RectTransform thisRect;
    private void Awake()
    {
         thisRect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {//게임오브젝트가 활성화될떄
        canPlaying = true;
        StartCoroutine(AutoScroll());
        
    }

    private void OnDisable()
    {//게임오브젝트가 비활성화될때
        canPlaying = false;
        thisRect.offsetMax = new Vector2(0, 0);
    }
    IEnumerator AutoScroll()
    {
        

        while (canPlaying)
        {
            //thisRect.offsetMax = new RectOffset(0, 0, interval, 0);
            thisRect.offsetMax += new Vector2(0, interval);
            //interval +=interval;
            yield return null;
        }
        
        yield break;
    }
}
