using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : Interactable {
    [SerializeField] private GameObject button;
    [SerializeField] private string[] passwords;
    private ILevel level;

    // Use this for initialization
    public void Start () {
        level = GameObject.Find("Level").GetComponent<ILevel>();
	}

    public override void Interact(Player player) {

        // Tell level to open password entry UI
        level.OpenPasswordEntry(passwords);

    }

}
