using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLevel : MonoBehaviour, ILevel {
	[SerializeField] private Text objective;
	[SerializeField] private GameObject toPyramids;

	// Use this for initialization
	void Start () {
		objective.text = "Steal a weapon";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// return true when the player has completed all objectives
	public bool ObjectivesComplete() {
		return true;
	}
}
