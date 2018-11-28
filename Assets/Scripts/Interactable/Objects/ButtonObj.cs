using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObj : Interactable {
    [SerializeField] private GameObject button;
    private ILevel level;

    private void Start() {
        level = GameObject.Find("Level").GetComponent<ILevel>();
    }

    public override void Interact(Player player) {
        // Hide the button after interaction;
        button.SetActive(false);

        // Pass button to level for handling
        level.ButtonPressed(button.name);
    }
}
