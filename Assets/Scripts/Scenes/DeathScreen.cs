using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour {

    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

	// Use this for initialization
	void Start () {
        // Add listeners for buttons
        restartButton.onClick.AddListener(RestartLevel);
        quitButton.onClick.AddListener(QuitGame);

        // Unlock the mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Restart the current level
    private void RestartLevel() {
        SceneManager.LoadScene(PlayerPrefs.GetString("currentScene"), LoadSceneMode.Single);
    }

    // Exit the game
    private void QuitGame() {
        Application.Quit();
    }
}
