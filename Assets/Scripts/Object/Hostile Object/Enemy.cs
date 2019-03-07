using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Unit {
    //유닛의 고유 패턴을 담당하는 추상 클래스이다.
    public float forceConstant = 1f;
    public GameObject[] dropObject;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody2D rgd;
    [HideInInspector]
    public HealthDisplay thisHealth;
    //무기아이템에서 이 컴포넌트를 액세스해야하므로 public으로 선언한다.
    protected override void Property(UnitActionState value)
    {
        if (!(ChangeState(value)))
            return;
        u_A_S = value;
        if (value == UnitActionState.PROTECT)
        {
            animator.SetTrigger("UnitProtect");
            StartCoroutine(SetU_A_SProtect(0.14f));
            UnitHitEvent();
            return;
        }
        if (value == UnitActionState.DEATH)
        {
            if (dropObject !=null)
                foreach (GameObject o in dropObject)
                    Instantiate(o, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            return;
        }
        if (value == UnitActionState.PATTERN)
        {
            UnitPatrolPattern();
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        thisHealth = GetComponent<HealthDisplay>();
        rgd = GetComponent<Rigidbody2D>();
        gameObject.tag = "Enemy";
        Parent_Awake();
    }

    protected virtual void Parent_Awake() { }

    private void Update()
    {
        if (Pattern_StartCondition())
            UnitAttackPattern();
        else
            UnitPatrolPattern();
    }

    protected abstract void UnitAttackPattern();

    protected abstract void UnitPatrolPattern();

    /// <summary>
    /// 보스에네미는 시야에 없어도 예외로 패턴을 사용하는 경우가 많기 때문에 virtual로 설정했다. 패턴을 시작할 조건 함수로 bool을 반환한다.
    /// </summary>
    /// <returns></returns>
    protected virtual bool Pattern_StartCondition()
    {
        if (PlayerManager.Player.transform.position.x - transform.position.x <= 5&& PlayerManager.Player.transform.position.x - transform.position.x >= -5)
            return true;//적이 플레이어 시야에 완전히 들어오면 패턴을 시작

        return false;
        //잡몹들의 패턴을 만들때는 이 함수를 따로 재정의하지 않는다.
    }

    protected virtual void UnitHitEvent()
    {

    }

}
