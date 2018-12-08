using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookObjective : Objective {

	// Use this for initialization
	void Start () {
        description = "Find the Book";
        failureText = "You didn't read the book of the Pharaoh's weaknesses?...\n\n";
	}
	
    // Player read the book
	public void ReadBook() {
        complete = true;
    }
}
