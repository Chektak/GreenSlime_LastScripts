using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuperManager : MonoBehaviour {
    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
