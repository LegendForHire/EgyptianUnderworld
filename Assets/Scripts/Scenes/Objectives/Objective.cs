using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : MonoBehaviour {
    protected string description;
    protected string failureText;
    protected bool complete = false;

    // Return this objective's description
    internal virtual string GetDescription() {
        return description;
    }

    // Determine if this objective is complete
    internal virtual bool IsComplete() {
        return complete;
    }

    // Get the message displayed for failing this objective
    internal virtual string GetFailureText() {
        return failureText;
    }
}
