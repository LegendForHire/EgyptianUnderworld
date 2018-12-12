using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Written by Ryan Kugel & Wibbs
 */
public class TutorialLevel : Level {
    [SerializeField] private Text countdown;
    private bool countdownStarted = false;
    private int timeLeft = 60;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        infoText = new List<string> {
            "Hello my pawn. By now I'm sure you've noticed I'm in your head to stay.\n\nLet me explain how this arrangement is going to work. I need you to do things for me, and you will.\n\nI don't always care how you do them, but they must be done.",
            "This is all very simple. Do what I ask of you, and eventually I'll let your family go.\n\nFail, and I will kill your family... One by one.\n\nHere's your first task.",
            "In this village you need to steal a weapon from the town center, but the guards will attack you once you do so.\n\nYou MUST kill every guard in the town. We can't have any witnesses...\n\nFinally, escape to the pyramids where we will begin your training.\n\nOh... And you only have 60 seconds to reach the pyramids once you've stolen a weapon."
        };

        infoDisplay.textList = infoText;

        // Set needed player prefs values for family members
        PlayerPrefs.SetInt("daughterKilled", 0);
        PlayerPrefs.SetInt("wifeKilled", 0);
        PlayerPrefs.SetInt("sonKilled", 0);

        // Set this scene as current scene
        PlayerPrefs.SetString("currentScene", "TownCenter");

        // Set next level scene
        PlayerPrefs.SetString("nextScene", "Pyramids");

        // Set player's weapon to none
        PlayerPrefs.DeleteKey("playerWeapon");

        currentObjective = objectives[0];
        objectiveText.text = currentObjective.GetDescription();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        if (player.equipped != null && !countdownStarted) {
            countdownStarted = true;
            StartCoroutine(Countdown());
        }

        countdown.text = "Time Left: " + timeLeft;
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
    internal override void SetLevelResults() {
        // set success - 0 = false, 1 = true
        int success = 1;

        // set result text
        string resultsText = "";

        for (int i = 0; i < objectives.Count; i++) {
            if (!objectives[i].IsComplete()) {
                success = 0;
                resultsText += objectives[i].GetFailureText();
            }
        }

        // player finished in time?
        if (timeLeft < 1) {
            resultsText += "You didn't finish my instructions in time.\n\n";
            success = 0;
        }

        PlayerPrefs.SetInt("success", success);


        if (success == 1) resultsText += "Good work my pawn.\n\n";
        resultsText += "Let's move on...";
        PlayerPrefs.SetString("resultsText", resultsText);
    }

}
