using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button controlsButton;

    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private Button closeButton;

	// Use this for initialization
	void Start () {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        controlsButton.onClick.AddListener(ToggleControls);
        closeButton.onClick.AddListener(ToggleControls);
	}
	
    // Start the game
	private void PlayGame() {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

    // Quit the game
    private void QuitGame() {
        Application.Quit();
    }

    // Open or close the controls panel
    private void ToggleControls() {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }
}
