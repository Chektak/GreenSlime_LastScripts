using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ShieldLeaf : Item {

    bool isUsed = false;
    protected override void UseItem()
    {
        Debug.Log(isUsed);
        Rigidbody2D thisrgd = GetComponent<Rigidbody2D>();
        if (isUsed == false)
        {
            isUsed = true;
            thisrgd.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            isUsed = false;
            thisrgd.constraints = RigidbodyConstraints2D.None;
        }
        OriginalEulerChange();
    }
    private new void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Rigidbody2D rgd = coll.gameObject.GetComponent<Rigidbody2D>();
            float directionX = transform.position.x - PlayerManager.Player.transform.position.x;
            Vector2 direction = new Vector2(directionX, 0);
            direction.Normalize();
            direction += new Vector2(0, 1.5f);
            rgd.AddForce(direction * rgd.mass * knockback);
        }
    }
}
