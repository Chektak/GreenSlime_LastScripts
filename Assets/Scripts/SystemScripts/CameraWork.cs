using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour {
    Vector3 thispos;

    public float followSpeed;

    
    void Start () {
        if(PlayerManager.Player!=null)
        thispos = PlayerManager.Player.gameObject.transform.position - transform.position;
	}
	
	void LateUpdate () {
        if(PlayerManager.Player!=null)
            transform.position = Vector3.Lerp(transform.position, PlayerManager.Player.transform.position - thispos, Time.deltaTime * followSpeed);
	}
}
