using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    //public CanvasGroup canvasGroup;
    public Transform pause;
    public Image fade;

    public void Start() {
        fade.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    public void Update() {
        // Reveals cursor
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            pause.gameObject.SetActive(true);
        }
    }

    public void LoadGame() {
        StartCoroutine(SceneTransition());
    }

    private IEnumerator SceneTransition() {
        fade.enabled = true;
        Return();
        float fadeVal = 0;
        while (fade.GetComponent<CanvasRenderer>().GetAlpha() < 1) {
            fadeVal += Time.deltaTime / 2;
            fade.GetComponent<CanvasRenderer>().SetAlpha(fadeVal);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    public void Return() {
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
