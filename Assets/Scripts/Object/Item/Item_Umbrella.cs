using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Umbrella : Item
{
    public float flyForce = 15;
    public float flyCoolTime = 1.25f;
    bool canFly = true;
    
    protected override void Start()
    {
        base.Start();
        
    }
    protected override void Update()
    {
        base.Update();
        if (objItem.isEnable == false)
        {
            objItem.naturalColl.isTrigger = true;
        }
    }

    protected override void UseItem()
    {
        if (canFly == true)
        {
            StartCoroutine(Fly());
        }
    }
    
    IEnumerator Fly()
    {
        canFly = false;
        Rigidbody2D playerRgd = PlayerManager.Player.GetComponent<Rigidbody2D>();
        playerRgd.AddForce(Vector2.up*flyForce);
        float cool = flyCoolTime;
        while (cool > 0)
        {
            cool -= Time.deltaTime;
            yield return null;
        }
        PlayerManager.Player.Setupp_A_S(Unit.UnitActionState.IDLE);
        canFly = true;
        yield break;
    }

}
