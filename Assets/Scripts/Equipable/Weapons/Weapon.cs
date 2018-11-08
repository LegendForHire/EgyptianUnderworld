using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour,  Equipable{

	// Use this for initialization
	protected virtual void Start () {
		
	}

    // Update is called once per frame
    protected virtual void Update () {
		
	}
    public abstract void playerAttack(Player player);
    public abstract void enemyAttack();
}
