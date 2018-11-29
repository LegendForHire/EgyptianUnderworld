using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {
    public Player player;
	// Use this for initialization
	public virtual void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}
    public abstract void OnCollisionEnter(Collision collision);

    public abstract float GetDamage();
   
}
