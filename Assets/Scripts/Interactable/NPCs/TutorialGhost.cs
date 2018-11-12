using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhost : NPC {
    public override void Interact(Player player)
    {
        StartCoroutine(Tutorial());

    }
    private IEnumerator Tutorial()
    {
        yield return null;
    }
}
