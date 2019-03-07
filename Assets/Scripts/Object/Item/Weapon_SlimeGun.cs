using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_SlimeGun : Item {
    public GameObject bullet;
    public float bulletForce = 1f;
	protected override void UseItem()
    {
        GameObject shotBullet = Instantiate(bullet, new Vector3(
                transform.position.x, transform.position.y, 0), Quaternion.identity);
        Rigidbody2D rgd = shotBullet.GetComponent<Rigidbody2D>();
        if (PlayerManager.Player.P_S_S == PlayerManager.PlayerSeeState.SEELEFT)
        {
           
            rgd.AddForce(new Vector2(-1, 0) * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            
            rgd.AddForce(new Vector2(1, 0) * bulletForce, ForceMode2D.Impulse);

        }
        StartCoroutine(ShotCoolTime(0.5f));
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        if (coll.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator ShotCoolTime(float cool)
    {
        float time = 0;
        while (time < cool)
        {
            time+=Time.deltaTime;
            yield return null;
        }
        OriginalEulerChange();
        yield break;
    }
}
