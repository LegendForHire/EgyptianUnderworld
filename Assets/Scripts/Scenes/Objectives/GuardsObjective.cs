using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsObjective : Objective {
    [SerializeField] private List<GuardObjective> guards;

    // Use this for initialization
    void Start() {
        description = "Kill the Guards";
        failureText = "You failed to kill all of the guards.\n\n";
    }

    // Update is called once per frame
    void Update () {
        bool guardsLeft = false;

		for (int i = 0; i < guards.Count; i++) {
            if (guards[i].alive) {
                guardsLeft = true;
                break;
            }
        }

        complete = !guardsLeft;
	}
}
