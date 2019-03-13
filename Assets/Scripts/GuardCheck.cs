using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCheck : MonoBehaviour {

    private bool triggered;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && triggered) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            triggered = false;
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

}
