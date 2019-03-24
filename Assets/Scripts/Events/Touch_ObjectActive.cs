using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_ObjectActive : MonoBehaviour {
    public GameObject[] gameObjects;
    bool canActive = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&canActive==true)
        {
            canActive = false;
            foreach (GameObject o in gameObjects)
            {
                o.SetActive(true);
            }
        }
    }
}
