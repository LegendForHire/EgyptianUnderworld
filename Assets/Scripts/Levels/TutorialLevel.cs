﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLevel : MonoBehaviour, ILevel {
    [SerializeField] private InfoDisplay infoDisplay;
	[SerializeField] private Text objective;
    [SerializeField] private Image crosshairs;
    [SerializeField] private Text countdown;
    [SerializeField] private Image health;
    [SerializeField] private Text healthText;

	[SerializeField] private GameObject toPyramids;

    private int enemiesLeft = 8;
    private int timeLeft = 45;
    private bool countdownStarted = false;
    private bool hasWeapon = false;

    private List<string> objectives = new List<string> { "Steal a weapon.", "Kill the guards.", "Escape to the pyramids!" };
    private List<string> infoText = new List<string> {
        "Hello my pawn. By now I'm sure you've noticed I'm in your head to stay.\n\nLet me explain how this arrangement is going to work. I need you to do things for me, and you will.\n\nI don't always care how you do them, but they must be done.",
        "This is all very simple. Do what I ask of you, and eventually I'll let your family go.\n\nFail, and I will kill your family... One by one.\n\nHere's your first task.",
        "In this village you need to steal a weapon from the town center, but the guards will attack you once you do so.\n\nYou MUST kill every guard in the town. We can't have any witnesses...\n\nFinally, escape to the pyramids where we will begin your training.\n\nOh... And you only have 45 seconds to reach the pyramids once you've stolen a weapon."
    };

    private int currentObjective = 0;

    // Use this for initialization
    void Start () {
        infoDisplay.textList = infoText;

        // Set needed player prefs values for family members
        PlayerPrefs.SetInt("daughterKilled", 0);
        PlayerPrefs.SetInt("wifeKilled", 0);
        PlayerPrefs.SetInt("sonKilled", 0);
    }
	
	// Update is called once per frame
	void Update () {
        // hide HUD when the info display is still up
        UpdateHUD(!infoDisplay.isActiveAndEnabled);
    }

	// Return true when the player has completed all objectives
    // Here, the player doesn't NEED to complete any of objectives to continue
    // Not completing the objectives will affect gameplay, however.
	public bool ObjectivesComplete() {
        return true;
	}

    // Player killed a guard, decrement enemies left
    public void GuardKilled() {
        enemiesLeft--;

        // if the player has killed all enemies switch the objective
        if (enemiesLeft == 0) {
            currentObjective = 2;
        }
    }

    // Player picked up weapon, switch objective & start countdown
    public void GotWeapon() {
        if (currentObjective == 0) {
            hasWeapon = true;
            currentObjective = 1;
            countdownStarted = true;
            StartCoroutine(Countdown());
        }
    }

    // Update elements of the HUD
    private void UpdateHUD(bool showHUD) {
        objective.text = objectives[currentObjective];
        countdown.text = "Time Left: " + timeLeft;

        // Show or hide HUD elements
        objective.gameObject.SetActive(showHUD);
        crosshairs.gameObject.SetActive(showHUD);
        countdown.gameObject.SetActive(countdownStarted);
        health.gameObject.SetActive(showHUD);
        healthText.gameObject.SetActive(showHUD);
    }

    // Countdown timer for this level
    private IEnumerator Countdown() {
        while (timeLeft > 0) {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;

        }

        // Countdown is over
        countdown.color = new Color(229, 0, 0);
    }

    // Store the players results this level in player prefs
    public void SetLevelResults() {
        // set success - 0 = false, 1 = true
        int success = 1;
        if (enemiesLeft > 0 || timeLeft < 1 || !hasWeapon) success = 0;
        PlayerPrefs.SetInt("success", success);

        // set result text
        string resultsText = "";

        if (enemiesLeft > 0) resultsText += "You failed to kill all of the guards.\n\n";
        if (timeLeft < 1) resultsText += "You didn't finish my instructions in time.\n\n";
        if (!hasWeapon) resultsText += "You couldn't even steal a weapon?\n\n";

        if (resultsText == "") resultsText += "Good work my pawn.\n\n";
        resultsText += "Let's move on...";
        PlayerPrefs.SetString("resultsText", resultsText);

        // set next level scene
        PlayerPrefs.SetString("nextScene", "Pyramids");
    }
}
