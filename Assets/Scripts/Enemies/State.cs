/*
 * State.cs
 * Written by Shannon Duvall
 * Abstract super class defining a state
 * for the Enemy AI.
 */ 
using UnityEngine;
using System.Collections;

public abstract class State{

	// A reference to the containing class.
	protected Enemy enemy;

	public State(Enemy enemyObj){
		enemy = enemyObj;
	}

	// Each state MUST have an update
	public abstract void Update (); 

	// States may or may not override this.
	public virtual void OnTriggerEnter(Collider other){
	}
}
