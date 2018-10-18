using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour {
    Vector3 thispos;

    public GameObject target;
    public float followSpeed;
	void Awake () {
        thispos = target.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position - thispos, Time.deltaTime * followSpeed);
	}
}
