using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCutscene : MonoBehaviour {

    public Image fade;
    public Transform player;
    public Transform boss;
    public GameObject particle;
    public GameManager GM;

    [Space(5)]
    public DialogueTrigger humanEnd;
    public DialogueTrigger demonEnd;

    private float angle;
    private bool triggered = true;

    public void Start() {
        fade.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    public void Update() {
        if (Vector3.Distance(player.position, boss.position) < 7) {
            float step = 2 * Time.deltaTime;
            // Rotate the boss toward the player
            Vector3 bossRotation = Vector3.RotateTowards(boss.forward, player.position - boss.position, step, 0.0f);
            bossRotation.y = 0;
            boss.rotation = Quaternion.LookRotation(bossRotation);

            // rotate the player toward the boss
            player.GetComponent<Controller>().enabled = false;
            Vector3 playerRotation = Vector3.RotateTowards(player.forward, boss.position - player.position, step, 0.0f);
            playerRotation.y = 0;
            player.rotation = Quaternion.LookRotation(playerRotation);

        }

        if (Input.GetKeyDown(KeyCode.E) && !triggered) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }

        if(GM.shrineCount == GM.shrinesFound && !GM.human) {

            particle.SetActive(true);
        }

        if (GM.dialogueFinished && !triggered) {
            StartCoroutine(FadeOut());
            //SceneManager.LoadScene("EndScreen");
        }
    }

    public void FixedUpdate() {
        angle = Vector3.Angle(boss.forward, player.position - boss.position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(boss.position, player.position - boss.position, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
            if (hit.transform.tag == "Player" && angle < 10 && triggered) {
                if (GM.shrineCount == GM.shrinesFound && !GM.human) demonEnd.TriggerDialogue();
                else humanEnd.TriggerDialogue();
                triggered = false;
            }
        }
    }

    private IEnumerator FadeOut() {
        fade.enabled = true;
        float fadeVal = 0;
        while (fade.GetComponent<CanvasRenderer>().GetAlpha() < 1) {
            //Debug.Log(fadeVal);
            fadeVal += Time.deltaTime / 2;
            fade.GetComponent<CanvasRenderer>().SetAlpha(fadeVal);
            yield return null;
        }
        SceneManager.LoadScene("EndScreen");
        yield return null;
    }

}
