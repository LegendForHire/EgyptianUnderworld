using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharaohBook : Interactable {
	private ILevel level;
	private List<string> text = new List<string> {
		"The Pharaoh's Weaknesses",
		"The Pharaoh is protected only by his guards and the traps set throughout his palace.\n\nIf someone is able to overcome these obstacles, the Pharaoh should be no problem to kill.",
		"The Pharaoh is known for placing his trust in the wrong people. Especially his second in command, the Vizier.\n\nThere have been many rumors that the Vizier has plotted against him.",
		"The Pharaoh is oblivious that trusted individuals may be planning to overthrow him."
	};

	// Use this for initialization
	void Start () {
		level = GameObject.Find("Level").GetComponent<ILevel>();
	}

	public override void Interact(Player player) {
		// Tell the level to open the dialog box
		level.OpenTextDialog(text);
		level.AdvanceObjective();
		level.ReadBook();
	}
	
}
