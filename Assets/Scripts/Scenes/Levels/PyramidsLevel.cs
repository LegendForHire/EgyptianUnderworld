using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *   Written by Ryan Kugel
 */
public class PyramidsLevel : Level {
    [SerializeField] private PasswordEntry passwordEntry;
    [SerializeField] private GameObject finalDoor;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        infoText = new List<string> {
            "Hello again my pawn. You've now entered one of the Pyramids of Giza.\n\nUltimately you will need find the book of the Pharaoh's weaknesses, which is guarded over a spirit. DO NOT talk to this spirit. It cannot be trusted.",
            "Throughout the pyramid you will find several obstacles that stand in your way.\n\nPits of lava that will kill you instantly separate you from some of the chambers. You may need to find some way of crossing these.\n\nThere could be some buttons or switches that will help you.",
            "I need you to get the book. So go find it!"
        };

        passwordEntry.gameObject.SetActive(false);
        infoDisplay.textList = infoText;

        // Set this scene as current scene
        PlayerPrefs.SetString("currentScene", "Pyramids");

        // set next level scene
        PlayerPrefs.SetString("nextScene", "Cairo");

        currentObjective = objectives[0];
        objectiveText.text = currentObjective.GetDescription();
    }

    // Set an array of GameObjects visible or invisible
    private void SetObjectsVisibility(GameObject[] objs, bool visible) {
        for (int i = 0; i < objs.Length; i++) {
            objs[i].SetActive(visible);
        }
    }

    // Open the info dialog with a given list of text
    public void OpenTextDialog(List<string> text) {
        infoDisplay.textList = text;
        infoDisplay.currentItem = 0;
        infoDisplay.OpenDisplay();
    }

}
