using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ShieldLeaf : Item {
    public Sprite idleSprite;
    public Sprite usingSprite;
    bool isUsed = false;

    protected override void UseItem()
    {
        if (isUsed == false)
        {
            isUsed = true;
            base.itemRenderer.sprite = usingSprite;
        }
        else
        {
            isUsed = false;
            base.itemRenderer.sprite = idleSprite;
        }
        OriginalEulerChange();
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (isUsed == true)
        {
            base.TriggerStay2D(coll);
        }
    }
}
