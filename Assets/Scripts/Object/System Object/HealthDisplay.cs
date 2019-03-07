using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {
    //대상 유닛의 체력과 체력UI를 관리하는 책임을 가진 스크립트이다.
    public SpriteRenderer[] healths;
    
    public double Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value <= 0)
            {
                unitState.SetToDeath();
            }
            if (value > healths.Length)
            {
                value = healths.Length;
            }
            health = value;
            ChangeSprite();
           
        }
    }
    //체력이 0이하로 내려가면, 오브젝트는 삭제되고 체력최대치를 초과한 힐량이라면, 힐을 취소하는 프로퍼티이다.

    private double health;
    private Unit unitState;
    private GUIManager guiManager = null;

    private void Awake()
    {
        unitState = GetComponent<Unit>();
  
    }
    private void Start()
    {
        health = healths.Length;
        SuperManager superManager;
        GameManager.Instance.managers.TryGetValue("GUIManager", out superManager);
        guiManager = superManager.gameObject.GetComponent<GUIManager>();
        InstantiateHeart();

    }//하트를 만든다

    private void ChangeSprite()
    {
        for (int fh = 0; fh < healths.Length; fh++)
        {//하트가 채워질경우가 있으니 먼저 체력을 FULL로 한다.
            healths[fh].sprite = guiManager.heart_Full;
        }
        int i = 0;
        double EmptyHeart = healths.Length - health;
        for (i=0; i<EmptyHeart; i++)
        {//정수형 데미지
            if(health>0)
                 healths[i].sprite = guiManager.heart_Empty;
        }
        
        if (EmptyHeart - (int)EmptyHeart > 0&&health>0)//부동소수점형 데미지
            healths[i-1].sprite = guiManager.heart_HalfEmpty;
    }

    /// <summary>
    /// guiManager에게서 하트스프라이트를 받고 포지션을 정하는 알고리즘을 통해 하트들을 인스턴스화한다.
    /// </summary>
    private void InstantiateHeart()
    {
        float interval = 0.5f;//하트사이의 간격
        float height = 1;
        float weight = 0;
        float first = 0;
        float last = healths.Length * interval;
        float middle;
        while (true)
        {
            first += interval;

            if (first == last)
            {
                middle = first;
                break;
            }
            if (first == last - (1 * interval))
            {
                middle = first + (interval / 2);
                break;
            }
            last -= interval;

            if (first > 100)
            {
                middle = 0;
                Debug.Log("This BUG!");
                break;
            }
        }
        for(int i=0; i<healths.Length; i++)
        {
            if (i == 10 * height)
            {
                height++;
                weight = 0;
            }
            weight++;
            healths[i].gameObject.transform.localPosition = new Vector2(weight*interval- middle, height);

            healths[i].sprite = guiManager.heart_Full;
        }
    }
}
