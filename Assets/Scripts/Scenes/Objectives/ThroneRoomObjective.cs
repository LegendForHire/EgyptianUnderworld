using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneRoomObjective : Objective {

	// Use this for initialization
	void Start () {
        description = "Get to the Throne Room";
	}
	
	// Entered the throne room
    public void EnteredThroneRoom() {
        complete = true;
    }
}
