using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CairoLevel : Level {
    //private List<string> objectives = new List<string> { "Get to the throne room", "Kill the Pharaoh" };

    // Use this for initialization
    protected override void Start() {
        base.Start();

        infoText = new List<string> {
            "My pawn, you've done well to make it this far.\n\nYou now stand before the great city of Cairo, where I give you your final instructions.",
            "You must kill the Pharaoh. It is time for his rule to end.\n\nThis will not be easy. Countless guards stand between you and the Pharaoh. It is your choice how to get past them. You may choose to evade them, or use...\nother methods...",
            "I will now give you one final gift.\n\nThe ability to leave your body, for a period. You can use this power by pressing the Q key.\n\nIt should help you get to the Pharaoh without dying.",
            "Go forth into the city. Find the Pharaoh, and kill him. I'll be watching..."
        };

        infoDisplay.textList = infoText;

        // Set this scene as the current scene
        PlayerPrefs.SetString("currentScene", "Cairo");

        currentObjective = objectives[0];
        objectiveText.text = currentObjective.GetDescription();
    }

    public void OpenTextDialog(List<string> text) {
        infoDisplay.textList = text;
        infoDisplay.currentItem = 0;
        infoDisplay.OpenDisplay();
    }

}
