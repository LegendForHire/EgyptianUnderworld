using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordEntry : MonoBehaviour {
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
    [SerializeField] private Player player;
    [SerializeField] private Text statusText;
    [SerializeField] private InputField passwordField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject passwordButton;
    [SerializeField] private GameObject obstacle;

    private string[] passwords;

    // Use this for initialization
    void Start () {
        submitButton.onClick.AddListener(CheckPassword);
        closeButton.onClick.AddListener(CloseDialog);
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        // don't let the player move while this is open
        firstPersonController.canMove = false;
        player.canUse = false;
    }

    // Display this dialog and make the cursor active
    public void OpenDialog() {
        passwordField.text = "";
        statusText.text = "Enter Password";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.gameObject.SetActive(true);
    }

    // Closes this dialog
    public void CloseDialog() {
        player.canUse = true;
        firstPersonController.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.SetActive(false);
    }

    // Setter for passwords
    public void SetPasswords(string[] passwords) {
        this.passwords = passwords;
    }

    // Check the user's given password against the correct answers
    private void CheckPassword() {
        string userAnswer = passwordField.text.ToLower().Trim();

        for (int i = 0; i < passwords.Length; i++) {
            // Correct answer
            if (userAnswer == passwords[i]) {
                passwordButton.SetActive(false);
                obstacle.SetActive(false);
                CloseDialog();
                return;
            }
        }

        // Incorrect answer
        statusText.text = "Incorrect Password";
    }
}
