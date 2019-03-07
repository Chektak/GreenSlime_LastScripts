using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy {
    private bool canjumpFuntion = true;

    protected override void Parent_Awake()
    {
        rgd = GetComponent<Rigidbody2D>();
    }

    protected override void UnitPatrolPattern()
    {
        int directionX= UnityEngine.Random.Range(1, 3);//1이상 3미만의 숫자를 랜덤으로 생성
        if (directionX == 2)
        {
            directionX = -1;
        }
        Jump(directionX, 1);
    }
    protected override void UnitAttackPattern()
    {
        Jump(PlayerManager.Player.transform.position.x - transform.position.x , 1.8f);
    }

    /// <summary>
    /// 공격과 이동모션이 같은 슬라임을 구현하기 때문에 directionX와 +형태인 Y값에대한 Force로 Jump를 구현했다.
    /// </summary>
    /// <param name="jumpForceY"></param>
    void Jump(float directionX,float jumpForceY)
    {
        if (canjumpFuntion == true)
        {
            StartCoroutine(JumpCoolTime(2f));
            Vector2 direction = new Vector2(directionX, jumpForceY);
            direction.Normalize();
            
            rgd.AddForce(direction * rgd.mass*forceConstant);
        }
    }
    IEnumerator JumpCoolTime(float duration)
    {
        canjumpFuntion = false;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canjumpFuntion = true;
        yield break;
    }
}
