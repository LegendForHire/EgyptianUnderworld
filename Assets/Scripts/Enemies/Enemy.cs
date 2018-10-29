using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    State currentState();
	// Use this for initialization
    public Enemy()
    {

    }
	virtual void Start () {

	}
    // Update behavior is off-loaded to the state.  Write the method.
    virtual void Update()
    {
        currentState.update();
    }

    // Trigger behavior is off-loaded to the state. Write the method.
    virtual void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
    abstract boolean isAlerted();
    abstract boolean spottedPlayer();
    abstract boolean playerInRange();
}
