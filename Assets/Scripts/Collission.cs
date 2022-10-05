using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collission : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameObject agent = GameObject.Find("Script");
        Debug.Log("Detected collision!");
        agent.GetComponent<PalletAgent>().CollisionDetection();
    }
}
