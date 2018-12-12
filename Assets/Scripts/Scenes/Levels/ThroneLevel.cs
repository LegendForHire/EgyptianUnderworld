using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneLevel : Level {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        infoText = new List<string> {};
        infoDisplay.textList = infoText;
        infoDisplay.gameObject.SetActive(false);

        currentObjective = objectives[0];
        objectiveText.text = currentObjective.GetDescription();
    }

    public void OpenTextDialog(List<string> text) {
        infoDisplay.textList = text;
        infoDisplay.currentItem = 0;
        infoDisplay.OpenDisplay();
    }

}
