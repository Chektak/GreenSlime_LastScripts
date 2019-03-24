using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_CameraBackColor : MonoBehaviour {
    public Color backGroundColor;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<Camera>().backgroundColor = backGroundColor;
        }
    }
}
