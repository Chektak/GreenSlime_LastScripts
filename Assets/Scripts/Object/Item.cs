using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {//모든 item들의 부모클래스이다.
    public string ItemTag = "Item";
    public string itemName;
    public int itemKey;
    public string itemTooltipText = "Name : tip : ";
    public float itemDamage = 1;
    public float knockback = 0;
    public AudioClip itemUseSoundEffect;
    private SpriteRenderer itemRenderer;//Inventory GUI에서 쓸 렌더러이다.
    // Use this for initialization
    protected virtual void Awake() { //virtual : 자식클래스에서 재정의안해도된다.
        itemRenderer = GetComponent<SpriteRenderer>();
	}

    private void Start()
    {
        gameObject.tag = ItemTag;
    }

    /// <summary>
    /// 상태변경에 성공했다면 아이템을 USE하는 함수를 실행한다.
    /// </summary>
    /// <param name="holder"></param>
    public void UsingTest()
    {
        if (PlayerManager.Player.ChangeState(Unit.UnitActionState.PATTERN))
        {//USE로 상태변경이 가능하다면
            PlayerManager.Player.Setupp_A_S(Unit.UnitActionState.PATTERN);
            transform.position = PlayerManager.Player.ReturnInventoryscript().itemHold.transform.position;
            SuperManager superManager;
            GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 사운드매니저에 대한 정보를 얻는다.
            SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
            soundManager.soundEffectSource.PlayOneShot(itemUseSoundEffect);
            UseItem();
            /* 매프레임마다 실행되는 함수가 아니라 아이템 사용 코루틴이 끝났는지 알수없어 *
             * 자식클래스 아이템사용코루틴에서 직접 플레이어상태를 IDLE로 변경해야한다. */
        }
    }

    /// <summary>
    /// UseItem : 자식클래스에서 꼭 재정의해야한다. 아이템 사용후엔 꼭 플레이어 상태를 IDLE로 변경해야한다. 
    /// 만약 아이템이 근접무기일경우 StartCoroutine동안 잠시 gameObject.tag를 "Weapon"으로 변경한다.
    /// </summary>
    /// <param name="holder"></param>
    protected virtual void UseItem() { }

    /// <summary>
    /// 이 스크립트를 상속하는 자식클래스의 렌더러를 반환한다.
    /// </summary>
    /// <param name="renderer"></param>
    public SpriteRenderer RendererInfo()
    {
        return itemRenderer;
    }

    /// <summary>
    /// 근접공격으로 아이템의 위치가 이상할때 아이템을 원래상태로 되돌리는 함수이다.
    /// </summary>
    public void OriginalEulerChange()
    {
        if (PlayerManager.Player.P_S_S == PlayerManager.PlayerSeeState.SEERIGHT)//본래 상태로 돌아간다.
            PlayerManager.Player.ReturnInventoryscript().itemHold.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            PlayerManager.Player.ReturnInventoryscript().itemHold.transform.rotation = Quaternion.Euler(0, 180, 0);

        gameObject.tag = "NowItem";
        transform.position=PlayerManager.Player.ReturnInventoryscript().itemHold.transform.position;
        PlayerManager.Player.U_A_S = PlayerManager.UnitActionState.IDLE;
    }

    public ObjItem ReturnObjItem()
    {
        return GetComponent<ObjItem>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy"&&this.gameObject.tag=="Weapon")
        {
            Unit unit = coll.gameObject.GetComponent<Unit>();
            Enemy enemy = (Enemy)unit;
            float directionX = transform.position.x - PlayerManager.Player.transform.position.x;
            Vector2 direction = new Vector2(directionX, 0);
            direction.Normalize();
            direction += new Vector2(0, 1.5f);
            enemy.rgd.AddForce(direction * enemy.rgd.mass * knockback);
            enemy.thisHealth.Health -= itemDamage;
            enemy.U_A_S = Enemy.UnitActionState.PROTECT;
        }
    }
}
