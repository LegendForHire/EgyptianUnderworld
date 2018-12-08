using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjective : Objective {
    [SerializeField] private Player player;

	// Use this for initialization
	void Start () {
        description = "Steal a Weapon";
        failureText = "You couldn't even steal a weapon?\n\n";
	}
	
	// Update is called once per frame
	void Update () {
		if (player.equipped != null) {
            complete = true;
        }
	}
}
