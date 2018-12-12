using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakToObjective : Objective {

	// Use this for initialization
	void Start () {
        description = "Speak to the Pharaoh";
	}
	
	// Spoke to the Pharaoh & Vizier
    public void SpokenToPharaoh() {
        complete = true;
    }
}
