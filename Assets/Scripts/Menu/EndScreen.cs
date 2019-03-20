using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    public CanvasGroup canvasGroup;

    public void LoadMainMenu() {
        StartCoroutine(SceneTransition());
    }

    private IEnumerator SceneTransition() {
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime / 1.5f;
            yield return null;
        }
        canvasGroup.interactable = false;
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}
