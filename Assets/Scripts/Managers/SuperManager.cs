using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManager : MonoBehaviour {
    protected GameManager gameManager = null;
    protected void Awake()
    {
        gameManager = FindObjectOfType(typeof(GameManager))as GameManager;
    }
    protected void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
