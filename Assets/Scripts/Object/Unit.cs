using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {
    public enum UnitActionState
    {
        IDLE,
        PATTERN,//유닛의 추상적인 행동 고유 패턴이다. 여기엔 플레이어가 아이템을 사용할때와 몬스터의 움직임 패턴 등이 해당된다.
        PROTECT,
        DEATH
    }
    public UnitActionState U_A_S
    {
        get
        {
            return u_A_S;
        }
        set
        {
            Property(value);//value에 대해 자식클래스의 set프로퍼티를 실행한다.
        }
    }
    public virtual void SetToDeath()
    {
        U_A_S = UnitActionState.DEATH;
    }
    protected abstract void Property(UnitActionState value);

    protected UnitActionState u_A_S=UnitActionState.IDLE;


    /// <summary>
    /// P_A_S를 Protect로 변경한다.(무적지속에 관련한 코루틴 함수이다.) 끝날시 P_A_S를 IDLE로 되돌린다.
    /// </summary>
    /// <param name="duration">무적지속시간</param>
    /// <returns></returns>
    /// 
    public IEnumerator SetU_A_SProtect(float duration)
    {
        string originalTag = gameObject.tag;
        float time = 0;
        while (time < duration)
        {
            gameObject.transform.tag = "Untagged";
            //공격을 받지않게 유닛의 태그를 변경한다.
            time += Time.deltaTime;
            yield return null;
        }
        Setupp_A_S(UnitActionState.IDLE);//무적시간이 지났으므로 원상태로 되돌린다.
        gameObject.transform.tag = originalTag;
        yield break;
    }


    /// <summary>
    /// 다른스크립트에서 보호수준때문에 p_A_S를 직접 할당할 수 없으므로 p_A_S를 할당해주는 함수이다.
    /// </summary>
    /// <param name="value"></param>
    public void Setupp_A_S(UnitActionState value)
    {
        u_A_S = value;
    }

    /// <summary>
    /// 코드가독성과 다른 클래스를 위해 정의한 함수이다. value로 P_A_S가 변할수 있다면 true를 리턴하고, 그외 false를 리턴한다.
    /// </summary>
    /// <param name="value">상태변경가능여부를 테스트하기위해 넣을 상태값</param>
    /// <returns></returns>
    public bool ChangeState(UnitActionState value)
    {//이 함수가 반환하는 값은 P_A_S프로퍼티에도 영향을 끼친다.
        if (U_A_S == UnitActionState.PROTECT || U_A_S == UnitActionState.DEATH || U_A_S == value)
            return false;
        else
            return true;
    }
}
