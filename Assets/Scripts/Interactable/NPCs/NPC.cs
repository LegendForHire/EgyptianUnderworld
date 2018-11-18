using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {
    [SerializeField] private string[] lines;
    private int currentline;
    public override void Start()
    {
        currentline = 0;
    }
	public override void Interact(Player p)
    {
        if (lines.Length > currentline)
        {
            Debug.Log(lines[currentline]);
            currentline++;
        }
        else if (currentline > 0){
            currentline = 0;
            Debug.Log(lines[currentline]);
            currentline++;
        }
    }
}
