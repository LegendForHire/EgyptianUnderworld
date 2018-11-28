using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PyramidsLevel : MonoBehaviour, ILevel {
    [SerializeField] private InfoDisplay infoDisplay;
    [SerializeField] private PasswordEntry passwordEntry;
    [SerializeField] private Text objective;
    [SerializeField] private Image crosshairs;
    [SerializeField] private Image health;
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject finalDoor;

    [SerializeField] private GameObject toCairo;

    private GameObject[] platformsA;
    private GameObject[] platformsB;
    private GameObject[] platformsC;

    private Dictionary<string, GameObject[]> buttonsToPlatforms = new Dictionary<string, GameObject[]>();

    private List<string> objectives = new List<string> { "Find the book" };
    private List<string> infoText = new List<string> {
        "Hello again my pawn. You've now entered one of the pyramids of giza.\n\nUltimately you will need find the book of the Pharaoh's weaknesses, which is guarded over a spirit. DO NOT talk to this spirit. It cannot be trusted.",
        "Throughout the pyramid you will find several obstacles that stand in your way.\n\nPits of lava that will kill you instantly separate you from some of the chambers. You may need to find some way of crossing these.\n\nThere could be some buttons or switches that will help you.",
        "I need you to get the book. So go find it!"
    };

    private int currentObjective = 0;
    private bool reachedEnd = true;
    private bool readBook = false;

    // Use this for initialization
    void Start () {
        passwordEntry.gameObject.SetActive(false);
        infoDisplay.textList = infoText;

        // Get all platforms in the level and hide them
        platformsA = GameObject.FindGameObjectsWithTag("PlatformA");
        platformsB = GameObject.FindGameObjectsWithTag("PlatformB");
        platformsC = GameObject.FindGameObjectsWithTag("PlatformC");

        SetObjectsVisibility(platformsA, false);
        SetObjectsVisibility(platformsB, false);
        SetObjectsVisibility(platformsC, false);

        // Add platforms arrays to dictionary
        buttonsToPlatforms.Add("ButtonA", platformsA);
        buttonsToPlatforms.Add("ButtonB", platformsB);
        buttonsToPlatforms.Add("ButtonC", platformsC);

        // Set this scene as current scene
        PlayerPrefs.SetString("currentScene", "Pyramids");
	}
	
	// Update is called once per frame
	void Update () {
        // hide the HUD when the info display or password entry are up
        bool showHUD = true;
        if (infoDisplay.isActiveAndEnabled || passwordEntry.isActiveAndEnabled) showHUD = false;

        UpdateHUD(showHUD);
	}

    // Set an array of GameObjects visible or invisible
    private void SetObjectsVisibility(GameObject[] objs, bool visible) {
        for (int i = 0; i < objs.Length; i++) {
            objs[i].SetActive(visible);
        }
    }

    // Button in the level was pressed, act accordingly
    public void ButtonPressed(string buttonName) {
        SetObjectsVisibility(buttonsToPlatforms[buttonName], true);
    }

    // Display the password entry UI
    public void OpenPasswordEntry(string[] passwords) {
        passwordEntry.SetPasswords(passwords);
        passwordEntry.OpenDialog();
    }

    // Update elements of the HUD
    private void UpdateHUD(bool showHUD) {
        objective.text = objectives[currentObjective];

        // Show or hide HUD elements
        objective.gameObject.SetActive(showHUD);
        crosshairs.gameObject.SetActive(showHUD);
        health.gameObject.SetActive(showHUD);
        healthText.gameObject.SetActive(showHUD);
    }

    // Store the players results this level in player prefs
    public void SetLevelResults() {
        string resultsText = "";
        int success = 1;

        // determine success & results text
        if (!reachedEnd) {
            success = 0;
            resultsText += "You failed to reach the end of the pyramid.\n\n";

        }
        if (!readBook) {
            success = 0;
            resultsText += "You didn't read the book of the Pharaoh's weaknesses.\n\n";
        }
        PlayerPrefs.SetInt("success", success);

        resultsText += "Let's move on...";
        PlayerPrefs.SetString("resultsText", resultsText);

        // set next level scene
        PlayerPrefs.SetString("nextScene", "Cairo");
    }

    public void GotWeapon() {
        throw new System.NotImplementedException();
    }

    public void GuardKilled() {
        throw new System.NotImplementedException();
    }

    public bool ObjectivesComplete() {
        return !finalDoor.activeSelf;
    }
}
