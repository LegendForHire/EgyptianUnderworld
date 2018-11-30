using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CairoLevel : MonoBehaviour, ILevel {
    [SerializeField] private Player player;
    [SerializeField] private InfoDisplay infoDisplay;
    [SerializeField] private Text objective;
    [SerializeField] private Image crosshairs;
    [SerializeField] private Image health;
    [SerializeField] private Text healthText;

    private List<string> objectives = new List<string> { "Get to the throne room", "Kill the Pharaoh" };
    private List<string> infoText = new List<string> {
        "My pawn, you've done well to make it this far.\n\nYou now stand before the great city of Cairo, where I give you your final instructions.",
        "You must kill the Pharaoh. It is time for his rule to end.\n\nThis will not be easy. Countless guards stand between you and the Pharaoh. It is your choice how to get past them. You may choose to evade them, or use...\nother methods...",
        "I will now give you one final gift.\n\nThe ability to leave your body, for a period. You can use this power by pressing the Q key.\n\nIt should help you get to the Pharaoh without dying.",
        "Go forth into the city. Find the Pharaoh, and kill him. I'll be watching..."
    };

    private int currentObjective = 0;
    private bool killedPharaoh = false;
    private bool killedVizier = false;
    private bool foundFamily = false;

    // Use this for initialization
    void Start() {
        infoDisplay.textList = infoText;

        // Set this scene as the current scene
        PlayerPrefs.SetString("currentScene", "Cairo");
    }

    // Update is called once per frame
    void Update() {
        // hide the HUD when the info display is up
        bool showHUD = true;
        if (infoDisplay.isActiveAndEnabled) showHUD = false;

        UpdateHUD(showHUD);
    }

    // Update elements of the HUD
    private void UpdateHUD(bool showHUD) {
        objective.text = objectives[currentObjective];
        player.canUse = showHUD;

        // Show or hide HUD elements
        objective.gameObject.SetActive(showHUD);
        crosshairs.gameObject.SetActive(showHUD);
        health.gameObject.SetActive(showHUD);
        healthText.gameObject.SetActive(showHUD);
    }

    // Advance the current objective variable
    public void AdvanceObjective() {
        currentObjective++;
    }

    public void ButtonPressed(string buttonName) {
        throw new System.NotImplementedException();
    }

    public void GotWeapon() {
        
    }

    public void GuardKilled() {

    }

    public bool ObjectivesComplete() {
        return (killedPharaoh || killedVizier || foundFamily);
    }

    public void OpenPasswordEntry(string[] passwords) {
        throw new System.NotImplementedException();
    }

    public void OpenTextDialog(List<string> text) {
        infoDisplay.textList = text;
        infoDisplay.currentItem = 0;
        infoDisplay.OpenDisplay();
        AdvanceObjective();
    }

    public void ReadBook() {
        throw new System.NotImplementedException();
    }

    public void SetLevelResults() {
        throw new System.NotImplementedException();
    }

}
