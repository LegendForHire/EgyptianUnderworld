using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLevel : MonoBehaviour, ILevel {
	[SerializeField] private Text objective;
	[SerializeField] private GameObject toPyramids;

    private int enemiesLeft = 3;

    private List<string> objectives = new List<string> { "Steal a weapon", "Kill the guards", "Escape to the pyramids!" };
    private int currentObjective = 0;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        objective.text = objectives[currentObjective];
    }

	// Return true when the player has completed all objectives
	public bool ObjectivesComplete() {
        return (currentObjective == objectives.Count - 1);
	}

    // Player killed a guard, decrement enemies left
    public void GuardKilled() {
        enemiesLeft--;

        // if the player has killed all enemies switch the objective
        if (enemiesLeft == 0) {
            currentObjective = 2;
        }
    }

    // Player picked up weapon, switch objective
    public void GotWeapon() {
        if (currentObjective == 0) {
            currentObjective = 1;
        }
    }
}
