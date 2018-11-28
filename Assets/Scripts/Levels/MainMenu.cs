using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

	// Use this for initialization
	void Start () {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
	}
	
    // Start the game
	private void PlayGame() {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

    // Quit the game
    private void QuitGame() {
        Application.Quit();
    }
}
