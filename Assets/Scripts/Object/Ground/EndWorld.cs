using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWorld : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            HealthDisplay playerHealth = coll.gameObject.GetComponent<HealthDisplay>();
            playerHealth.Health = 0;
        }
    }
}
