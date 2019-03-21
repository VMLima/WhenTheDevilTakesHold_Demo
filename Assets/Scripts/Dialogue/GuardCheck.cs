using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCheck : MonoBehaviour {

    public DialogueTrigger trueDialogue;
    public DialogueTrigger easterEgg;

    private bool triggered;
    private int count = 1;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && triggered) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (count < 6) {
                trueDialogue.TriggerDialogue();
                count++;
            } else if (count == 6) {
                easterEgg.TriggerDialogue();
                count = 0;
            }
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
