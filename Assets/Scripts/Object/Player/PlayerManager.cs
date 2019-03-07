using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerManager : Unit {
    //플레이어의 상태와 그 상태에 대한 애니메이션을 제어하고,  다른 플레이어스크립트를  책임을 갖는다.
    public enum PlayerSeeState
    {
        SEERIGHT,
        SEELEFT
    }

    public Animator animator;
    public PlayerSeeState P_S_S = PlayerSeeState.SEERIGHT;

    /// <summary>
    /// Unit부모클래스의 UnitActionState U_A_S의 set프로퍼티를 제어하기위한 함수.
    /// </summary>
    /// <param name="value"></param>
    protected override void Property(UnitActionState value)
    {
        if (!(ChangeState(value)))
            return;
        u_A_S = value;
        if (value == UnitActionState.PROTECT)
        {
            u_A_S = value;
            animator.SetTrigger("PlayerProtect");
            StartCoroutine(SetU_A_SProtect(0.5f));
            //애니메이션이 끝났는지를 반환하는 함수를 모르기때문에, 직접 애니메이션이 끝나는 시간(무적지속시간)을 측정해 인잣값으로 넣는다.
        }
        //else if (value == UnitActionState.PATTERN)
        //    u_A_S = value;//이 경우 Item클래스 UseItem()함수에서 애니메이션과 코루틴이 실행되므로 책임이 위임된다.
        else if (value == UnitActionState.DEATH)
        {
            u_A_S = value;
            DeathEvent();
        }
        //else if(value == UnitActionState.IDLE) u_A_S=value;
    }

    private Inventory inventoryscript = null;

    public static PlayerManager Player
    {
        get
        {
            return player;
        }
        
    }//몬스터 등 유닛이 접근하기위해 플레이어에도 싱글턴패턴을 적용한다.
    private static PlayerManager player = null;

    // Use this for initialization
    void Awake() {
        if (Player != null)
            return;
        player = this;
        inventoryscript = GetComponent<Inventory>();
        U_A_S = UnitActionState.PROTECT;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Item")
        {
            Item Item = coll.gameObject.GetComponent<Item>();
            
            foreach (Item o in inventoryscript.ReturnInventory())
            {
                if (o == null)
                {
                    Item.ReturnObjItem().EnableObjItem(false);//얻은 아이템을 비활성화
                    break;
                }
            }
            
            inventoryscript.AddInventory(Item);
            
        }
    }

    /// <summary>
    /// 다른스크립트에서는 보호수준때문에 액세스할 수 없으므로 inventory를 리턴해주는 함수이다.
    /// </summary>
    /// <returns></returns>
    public Inventory ReturnInventoryscript()
    {
        return inventoryscript;
    }

    /// <summary>
    /// 죽었을때 실행할 이벤트이다.
    /// </summary>
    private void DeathEvent()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(false);
        }////플레이어의 모든 Transform컴포넌트를 가진 오브젝트를 비활성화한다.
        {
            gameObject.SetActive(true);
            Rigidbody2D rgd = GetComponent<Rigidbody2D>();
            rgd.bodyType = RigidbodyType2D.Kinematic;
            Collider2D coll = GetComponent<Collider2D>();
            coll.enabled = false;
        }//Ghost상태(주요 기능을 잃은채 게임을 마음껏 돌아다닐 수 있다.)
        SuperManager superManager;
        GameManager.Instance.managers.TryGetValue("GUIManager", out superManager);//GameManager에서 GUI매니저에 대한 정보를 얻는다.
        GUIManager guiManager = superManager.gameObject.GetComponent<GUIManager>();
        guiManager.GetComponent<GUIController>().stageClearFailPanel.SetActive(true);
        
    }


}
