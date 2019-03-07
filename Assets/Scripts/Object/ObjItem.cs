using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjItem : MonoBehaviour {//오브젝트형으로 존재할 아이템에 붙이는 스크립트이다. Item스크립트와 공생한다.
    [HideInInspector]
    public Collider2D naturalColl;//몬스터와의 접촉을 판단할 콜라이더
    private Rigidbody2D rgd;
    private float outSpeed = 10f;
    // Use this for initialization
    void Awake()
    {
        rgd = gameObject.GetComponent<Rigidbody2D>();
        naturalColl = gameObject.GetComponent<PolygonCollider2D>();
        //오브젝트형으로 존재하고있는 아이템의 컴포넌트들의 정보를 얻는다.

    }
    /// <summary>
    /// 인자값에따라 오브젝트를 게임에서 제외시킬지 등장시킬지 결정한다.
    /// </summary>
    public void EnableObjItem(bool isenable)
    {
        if (isenable)//아이템을 뱉을때
        {
            rgd.drag = 0;
            if (PlayerManager.Player.P_S_S == PlayerManager.PlayerSeeState.SEERIGHT)
            {
                gameObject.transform.position =
                PlayerManager.Player.ReturnInventoryscript().itemHold.transform.position +
                new Vector3(1f, 0, 0);
                StartCoroutine(OutItem(new Vector2(1, 0)));
            }
            else if (PlayerManager.Player.P_S_S == PlayerManager.PlayerSeeState.SEELEFT)
            {
                gameObject.transform.position =
                PlayerManager.Player.ReturnInventoryscript().itemHold.transform.position +
                new Vector3(-1f, 0, 0);
                StartCoroutine(OutItem(new Vector2(-1, 0)));
            }
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            return;
        }
        //아이템을 먹을때
        //gameObject.GetComponent<Collider2D>().isTrigger = true;
        gameObject.transform.position = GameManager.Instance.outerWorld;
        rgd.drag = 1000000;//공기저항을 무한대로 늘려 오브젝트가 움직이지 않게한다.
            
    }

    /// <summary>
    /// 다른 스크립트에서 이 오브젝트에 접근할수 있도록 Item컴포넌트를 리턴한다.
    /// </summary>
    public Item ReturnItem(){
        return GetComponent<Item>();
    }
    
    /// <summary>
    /// 아웃아이템 함수 : 아이템을 뱉는다
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    IEnumerator OutItem(Vector2 direction)
    {
        float time = 0;
        float duration = 0.5f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rgd.AddForce(direction * rgd.mass * outSpeed);
            //시간이 지날수록 뱉는 힘이 약해진다.
        }
        yield break;
    }
}
