using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObj : Interactable {
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject[] platforms;

    // Hide platforms
    public override void Start() {
        for(int i = 0; i < platforms.Length; i++) {
            platforms[i].SetActive(false);
        }
    }

    public override void Interact(Player player) {
        // Hide the button after interaction;
        button.SetActive(false);

        // Show the platforms held by this button
        for (int i = 0; i < platforms.Length; i++) {
            platforms[i].SetActive(true);
        }
    }
}
