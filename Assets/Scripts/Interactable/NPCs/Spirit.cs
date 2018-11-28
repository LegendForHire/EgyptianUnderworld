using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : Interactable {
	private ILevel level;
	private List<string> text = new List<string> {
		"Hello, I am the spirit that guards over this pyramid.\n\nYou've made it through our maze, designed to keep all those who are unworthy out. I can only assume you've been infected by the Voice.",
		"Fear not for talking to me. This chamber is protected from his wicked ways.\n\nI cannot tell you who the Voice is, but I can tell you he is not to be trusted. He sent you to find the book of the Pharoah's weaknesses. It is here, sitting on the crate.",
		"If you choose to take the book, know that killing the Pharoah is not your only option.\n\nThe Voice has hidden your family somewhere in Cairo. If you find them we can transport you and your family away from Egypt, and away from the Voice.",
		"I do hope you found some clues to their location in this pyramid..."
	};

	// Use this for initialization
	void Start () {
		level = GameObject.Find("Level").GetComponent<ILevel>();
	}
	
	public override void Interact(Player player) {
		// Tell the level to open the dialog box
		level.OpenTextDialog(text);
	}
}
