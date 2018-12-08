using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTalk : MonoBehaviour {
    [SerializeField] private CairoLevel level;
    [SerializeField] ThroneRoomObjective objective;
    private bool opened = false;
	private List<string> text = new List<string> {
		"Pharaoh:\n\nOh, have you come here to kill me? Vizier, is this another one of your schemes?",
		"Vizier:\n\n...No... I know nothing of this plot...",
		"Pharaoh:\n\nLIAR. I know you've wanted to take the throne from me. It's a shame you didn't have the courage to do it yourself.",
		"Vizier:\n\nDo it, I have your family...",
		"Pharaoh:\n\nNo! Kill the Vizier and I can save your family!"
	};


	// Use this for initialization
	void Start () {

	}

	// Open final text dialog
	private void OnTriggerEnter(Collider other) {
        if (other.name == "Arrow" || opened) return;
		level.OpenTextDialog(text);
		opened = true;
        objective.EnteredThroneRoom();
	}

	
}
