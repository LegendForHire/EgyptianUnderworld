using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : Interactable {
    [SerializeField] private GameObject button;
    [SerializeField] private string[] passwords;
    [SerializeField] private PasswordEntry passwordEntry;

    public override void Interact(Player player) {
        passwordEntry.SetPasswords(passwords);
        passwordEntry.OpenDialog();
    }

}
