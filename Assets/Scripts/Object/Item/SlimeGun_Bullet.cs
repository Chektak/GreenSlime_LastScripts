using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGun_Bullet : Item {
    private void Update()
    {
        if (PlayerManager.Player.transform.position.x - transform.position.x >= 300f || PlayerManager.Player.transform.position.x - transform.position.x <= -300f)
            Destroy(gameObject);
    }
   protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        if(coll.gameObject.tag=="Ground")
        {
            Destroy(this.gameObject);
        }
    }

    
}
