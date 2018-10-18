using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float walkSpeed = 4f;
    public float jumpForce = 5f;
    public float attackdelay = 1f;

    private Vector3 pos=Vector3.zero;
    private Rigidbody2D thisrgd = null;
    private WeaponTake weaponscript = null;
    private bool isGrounded = false;
    private float isDoubleJumped = 0f;
    
	// Use this for initialization
	void Awake () {
        thisrgd = GetComponent<Rigidbody2D>();
        weaponscript = GetComponent<WeaponTake>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isGrounded)
        {
            if (Input.GetKeyDown("space") && isDoubleJumped>0)
            {
                thisrgd.velocity = new Vector2(0.0f, jumpForce);
                //rigidbody의 y방향에 대한 속도를 직접 바꿔쓴다.
                //thisrgd.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
                isDoubleJumped--;
            }
        }
        if (Input.GetKey("a"))
        {
            pos.x -= walkSpeed*Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += walkSpeed*Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            weaponscript.Attack(attackdelay);
        }
        transform.position += pos;
        pos = Vector3.zero;
	}

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isDoubleJumped = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            weaponscript.GetItemCode(coll);
        }
    }
}
