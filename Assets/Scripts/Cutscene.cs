using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Cutscene : MonoBehaviour {

    public Image fade;
    public Text text;
    public GameObject demonGuard;
    public GameObject guides;
    public GameManager GM;

    [Space(5)]
    public DialogueTrigger demon_Dialogue;
    public DialogueTrigger demon_Dialogue2;
    public DialogueTrigger player_Dialogue;
    public DialogueTrigger player_Dialogue2;
    public DialogueTrigger tutorial_Dialogue;

    [Space(5)]
    public DialogueManager DManager;

    [Space(5)]
    public AudioSource growl;

    [Space(5)]
    public MonoBehaviour[] disableScripts;

    [HideInInspector] public bool hidden;
    private bool first = true;
    private bool finalDialogue;
    private float tempSpeed;
    private Coroutine lastCoroutine;

    private void Awake() {
        ToggleScripts(false);
        fade.GetComponent<CanvasRenderer>().SetAlpha(1);
        StartCoroutine(FadeIn());
        tempSpeed = disableScripts[0].GetComponent<Controller>().speed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (disableScripts[0].enabled == true && Input.GetKeyDown(KeyCode.E)) {
            DManager.DisplayNextSentence();
        }

        if (hidden) {
            StopCoroutine(lastCoroutine);
            disableScripts[0].GetComponent<Controller>().temp = 0;
            demonGuard.SetActive(true);
            guides.SetActive(false);
            GM.human = true;
            if (demonGuard.transform.position.z > 0 && demonGuard.transform.position.z < 5 && first) {
                demon_Dialogue.TriggerDialogue();
                StartCoroutine(FinishHumanCutScene());
                first = false;
            }
        }

        if (GM.spotted && first) {
            StartCoroutine(FinishDemonCutScene());
            first = false;
        } else if (!GM.spotted && first && demonGuard.transform.position.z > 0 && demonGuard.transform.position.z < 5) {
            demon_Dialogue.TriggerDialogue();
            StartCoroutine(FinishHumanCutScene());
            first = false;
        }

        if(GM.dialogueFinished && finalDialogue) {
            this.gameObject.SetActive(false); // deactivate the tutorial
        }
    }

    // Happens on game load
    private IEnumerator FadeIn() {
        fade.enabled = true;
        float fadeVal = 1;
        while (fade.GetComponent<CanvasRenderer>().GetAlpha() > 0) {
            fadeVal -= Time.deltaTime / 2;
            fade.GetComponent<CanvasRenderer>().SetAlpha(fadeVal);
            yield return null;
        }
        fade.enabled = false;
        StartCoroutine(StartPlayerDialogue());
        yield return null;
    }

    // after fade in, trigger player asking where they are
    private IEnumerator StartPlayerDialogue() {
        player_Dialogue.TriggerDialogue();
        yield return new WaitForSeconds(3f);

        growl.Play();
        yield return new WaitForSeconds(7f); // wait for growl to mostly finish

        DManager.DisplayNextSentence();
        yield return new WaitForSeconds(2f);

        // Allow player to mover around (cannot exit room)
        disableScripts[0].enabled = true;
        text.enabled = true;
        tutorial_Dialogue.TriggerDialogue();

        // Start a count down for a potential demon path
        lastCoroutine = StartCoroutine(DemonPath());
    }

    // Human ending to the cutscene
    private IEnumerator FinishHumanCutScene() {
        yield return new WaitForSeconds(7f); // wait for demon to leave room

        hidden = false;
        disableScripts[0].GetComponent<Controller>().temp = tempSpeed;
        player_Dialogue2.TriggerDialogue();
        yield return new WaitForSeconds(3f); // not sure why this wfs but keep it, no time to figure out why

        ToggleScripts(true); // activate all the demons in the scene
        finalDialogue = true;
    }

    private IEnumerator DemonPath() {
        yield return new WaitForSeconds(30f);
        demonGuard.SetActive(true);
    }

    private IEnumerator FinishDemonCutScene() {
        demonGuard.GetComponent<NavMeshAgent>().isStopped = true;
        text.enabled = true;
        demon_Dialogue2.TriggerDialogue();
        yield return new WaitForSeconds(5f);
        demonGuard.GetComponent<NavMeshAgent>().isStopped = false;
        ToggleScripts(true);
        finalDialogue = true;
    }

    void ToggleScripts(bool value) {
        foreach(var script in disableScripts) {
            script.enabled = value;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            tempSpeed = disableScripts[0].GetComponent<Controller>().temp;
            disableScripts[0].GetComponent<Controller>().temp = 0;
            hidden = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
