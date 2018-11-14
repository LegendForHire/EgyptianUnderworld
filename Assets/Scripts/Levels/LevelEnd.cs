using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour {
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private Text successText;
    [SerializeField] private Text resultsText;
    [SerializeField] private Button resultsContinue;

    [SerializeField] private GameObject familyPanel;
    [SerializeField] private Text killText;
    [SerializeField] private Image daughter;
    [SerializeField] private Image wife;
    [SerializeField] private Image son;

    private bool success;

	// Use this for initialization
	void Start () {
        familyPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Get success from player prefs
        success = (PlayerPrefs.GetInt("success") == 1);

        if (success) successText.text = "Success";
        else successText.text = "Failure";

        // Get results from player prefs
        resultsText.text = PlayerPrefs.GetString("resultsText");

        // Add button listener
        resultsContinue.onClick.AddListener(ShowFamilyPanel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Show the family panel and update panel elements
    private void ShowFamilyPanel() {

        // Determine which family members are dead
        bool daughterKilled = (PlayerPrefs.GetInt("daughterKilled") == 1);
        bool wifeKilled = (PlayerPrefs.GetInt("wifeKilled") == 1);
        bool sonKilled = (PlayerPrefs.GetInt("sonKilled") == 1);

        if (success) {
            killText.text = "Good work. They can live... for now.";
        }

        // Determine which family member to kill
        else {
            if (daughterKilled) {
                if (sonKilled) {
                    // kill the wife
                    wifeKilled = true;
                    PlayerPrefs.SetInt("wifeKilled", 1);
                    killText.text = "Say goodbye to your wife!";

                    Debug.Log("FAMILY IS DEAD. GAME OVER");
                } else {
                    // kill the son
                    sonKilled = true;
                    PlayerPrefs.SetInt("sonKilled", 1);
                    killText.text = "It's shame we had to kill your son...";
                }
            } else {
                // kill the daughter
                daughterKilled = true;
                PlayerPrefs.SetInt("daughterKilled", 1);
                killText.text = "Now your little angel is an angel...";
            }
        }

        // Update family images with killed icons
        daughter.transform.Find("Killed").gameObject.SetActive(daughterKilled);
        wife.transform.Find("Killed").gameObject.SetActive(wifeKilled);
        son.transform.Find("Killed").gameObject.SetActive(sonKilled);

        familyPanel.SetActive(true);
    }
}
