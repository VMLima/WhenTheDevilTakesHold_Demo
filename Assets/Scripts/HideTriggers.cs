using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTriggers : MonoBehaviour {


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            gameObject.GetComponentInParent<Cutscene>().hidden = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
