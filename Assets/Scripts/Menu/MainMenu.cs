using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Transform mainMenu;
    public Transform credits;
    public CanvasGroup canvasGroup;

    public void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadGame() {
        //SceneManager.LoadScene("MazeLevel");
        StartCoroutine(SceneTransition());
    }

    public void Credits() {
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }

    public void Return() {
        mainMenu.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }

    private IEnumerator SceneTransition() {
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        SceneManager.LoadScene("MazeLevel");
        yield return null;
    }


}
