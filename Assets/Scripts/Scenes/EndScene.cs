using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {
    [SerializeField] private Button exitButton;

	// Use this for initialization
	void Start () {
        exitButton.onClick.AddListener(LoadMainMenu);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

    // Open the main menu scene
    private void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
