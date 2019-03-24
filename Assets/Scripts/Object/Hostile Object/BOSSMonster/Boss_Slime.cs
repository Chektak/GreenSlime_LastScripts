using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Slime : Enemy {
    public Color isAngryColor;
    private bool canJumpFuntion = true;
    private bool isAngry = false;
    private float jumpCooltime = 3;

    protected override void Parent_Awake()
    {
        rgd = GetComponent<Rigidbody2D>();
    }

    protected override void UnitPatrolPattern()
    {
        return;
    }

    protected override bool Pattern_StartCondition()
    {
        if(isAngry|| PlayerManager.Player.gameObject.transform.position.x - transform.position.x <= 6.5)
            return true;
        //화났거나 플레이어 시야에 들어왔다면
        return false;
    }
    protected override void UnitAttackPattern()
    {
        if (canJumpFuntion)
        {
            Jump(1);
            StartCoroutine(JumpCooltime(jumpCooltime));
            if (isAngry)
            {
                
               
                StartCoroutine(RandomJump(2f, PlayerManager.Player.transform.position.y-transform.position.y));

            }
                
        }
        

    }

    void Jump(float jumpForceY)
    {
        float directionX = PlayerManager.Player.transform.position.x - transform.position.x;
        Vector2 direction = new Vector2(directionX, jumpForceY);
        direction.Normalize();
        rgd.AddForce(direction * rgd.mass * forceConstant);
    }

    IEnumerator JumpCooltime(float cool)
    {
        canJumpFuntion = false;
        float time = 0;
        while (time < cool)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canJumpFuntion = true;
        yield break;
    }

    IEnumerator RandomJump(float duration,float jumpForceY)
    {
        float time = 0;
        float random;
        while (time < duration)
        {
            random = Random.Range(1, 20);//매 프레임을 기준으로 5%확률로 더블점프
            if (random == 1)
            {
                Jump(jumpForceY);
                yield break;
            }
            time += Time.deltaTime;
            yield return null;
        }
        Jump(jumpForceY);
        yield break;
    }
    protected override void UnitHitEvent()
    {//유닛이 데미지를 입었을때 이벤트
        if (thisHealth.Health <= thisHealth.healths.Length / 4 && isAngry == false)
        {
            isAngry = true;

            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<Camera>().backgroundColor = isAngryColor;

            jumpCooltime = 1.5f;
            forceConstant *= 3;
            for (int h = 0; h < 6; h++)
                thisHealth.Health++;
        }

    }
}
