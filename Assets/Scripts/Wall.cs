using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        GameObject agent = GameObject.Find("Script");
        agent.GetComponent<PalletAgent>().CollisionDetection();
    }
}
