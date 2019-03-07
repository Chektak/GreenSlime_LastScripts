using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HealPosion :Item {
    public float useAngle = 5f;
    public int itemHealAmount = 3;
    protected override void UseItem()
    {
        
        StartCoroutine(Eat_Posion());
    }
    IEnumerator Eat_Posion()
    {
        float time = 0;
        float duration = 0.15f;
        while (time < duration)
        {
            time += Time.deltaTime;
            PlayerManager.Player.ReturnInventoryscript().itemHold.transform.Rotate(0, 0, -(useAngle * Time.deltaTime));
            yield return null;
        }
        OriginalEulerChange();//슈퍼클래스의 본래 상태로 돌아가는 함수를 실행한다.
        yield return null;
        HealthDisplay playerHealth = PlayerManager.Player.gameObject.GetComponent<HealthDisplay>();
        playerHealth.Health += itemHealAmount;
        PlayerManager.Player.ReturnInventoryscript().RemoveInventory(this);
        yield return null;
        Destroy(this.gameObject);
        yield break;
    }
    
}
