using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_and_damage : MonoBehaviour {
    //몬스터나 함정에 사용되는 스크립트이다.
    public float knockbackConstant = 1;//닿았을때 얼마나 넉백될지
    public double damage = 1.0;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            
            HealthDisplay playerHealth=coll.gameObject.GetComponent<HealthDisplay>();
            playerHealth.Health -= damage;
            PlayerManager.Player.U_A_S = Unit.UnitActionState.PROTECT;
            {
                Vector2 direction = transform.position - coll.gameObject.transform.position;
                direction.Normalize();
                StartCoroutine(Knockback(direction, coll.rigidbody));
            }//넉백을 담당한다.
        }       
    }

    IEnumerator Knockback(Vector2 direction, Rigidbody2D rgd)
    {
        float time = 0;
        float duration = 0.5f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rgd.AddForce(-direction * knockbackConstant);
            //시간이 지날수록 뱉는 힘이 약해진다.
        }
        yield break;
    }
}
