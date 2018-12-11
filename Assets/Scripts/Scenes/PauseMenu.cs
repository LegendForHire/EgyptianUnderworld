using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
    [SerializeField] private Player player;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button closeButton;

	// Use this for initialization
	void Start () {
        restartButton.onClick.AddListener(RestartLevel);
        exitButton.onClick.AddListener(Exit);
        closeButton.onClick.AddListener(CloseDialog);
	}

    // Update is called once per frame
    void Update() {
        // don't let the move while this is open
        firstPersonController.canMove = false;
        player.canUse = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Open this dialog
    public void OpenDialog() {
        this.gameObject.SetActive(true);
    }

    // Close this menu
    public void CloseDialog() {
        player.canUse = true;
        firstPersonController.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.SetActive(false);
    }

    // Restart the current level
    private void RestartLevel() {
        string currentLevel = PlayerPrefs.GetString("currentScene");
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
    }

    // Exit to the main menu
    private void Exit() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
