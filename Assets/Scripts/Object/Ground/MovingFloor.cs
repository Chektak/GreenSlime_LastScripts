using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector3 minMovePosLimit = new Vector3();
    public Vector3 maxMovePosLimit = new Vector3();

    private bool canMoveRight = true;
    private Vector3 dir = new Vector3();

    void Start()
    {
        dir = Vector3.Normalize(maxMovePosLimit - minMovePosLimit);
        StartCoroutine(moving(canMoveRight));
    }

    IEnumerator moving(bool canMoveRight)
    {
        while(true){
            if (canMoveRight == true)
            {
                transform.position += dir * moveSpeed * Time.deltaTime;
                if (transform.position.x >= maxMovePosLimit.x)
                {
                    //transform.position = maxMovePosLimit;
                    canMoveRight = false;
                }
            }
            else
            {
                transform.position -= dir * moveSpeed * Time.deltaTime;
                if (transform.position.x <= minMovePosLimit.x)
                {
                    //transform.position = minMovePosLimit;
                    canMoveRight = true;
                }
            }
            yield return null;
        }
    }
}
