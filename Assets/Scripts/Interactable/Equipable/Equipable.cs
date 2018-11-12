using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipable : Interactable {
    public abstract void playerUse(Player player);
    public override void Interact(Player player)
    {
        player.Equip(this);
    }
}
