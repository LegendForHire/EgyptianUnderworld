﻿/*
 * Enemy.cs
 * Written by Shannon Duvall and <Your Name Here>.  
 * (You BETTER put your name there and not leave "Your Name Here" 
 * in these freaking comments!)
 * 
 * This code is borrowed heavily from StatePatternEnemy, written 
 * by Unity Technologies, from the Survival Shooter tutorial.
 */
using UnityEngine;
using System.Collections;
using System;

public abstract class Enemy : MonoBehaviour 
{
	// When on alert, this is the turning speed and 
	// search time before giving up.


	// This deals with all the navigation between way points.
	internal UnityEngine.AI.NavMeshAgent navMeshAgent;
	// The points the enemy travels between
    public Transform[] wayPoints;
	// And where we are on the path
	private int nextWayPoint;
    // Empty Game object created at eye level
    public Transform eyes;
	// Lift the look vector so he looks for the head, not the feet when chasing the player.
	// Otherwise, it is too hard to hide from the enemy.
	private Vector3 offset = new Vector3 (0,.5f,0);
    [SerializeField] public Player player;
    // Store ref to player transform so I know where to chase to.
    private Transform chaseTarget;
    internal State currentState;
    internal float searchTimer;
    internal float sightRange;
    internal float searchingDuration;
    internal Transform lastSeenOrHeard;
    internal float attackRange;
    internal float attackRate;
    private ILevel level;
    [SerializeField] private Weapon weapon;


    internal virtual void Awake()
	{
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        level = GameObject.Find("Level").GetComponent<ILevel>();

    }

	public virtual void Update () 
	{
        currentState.Update();
	}

    internal virtual bool PlayerInRange(){
       Debug.Log(Math.Abs(Vector3.Distance(this.transform.position, player.transform.position)));
       return Math.Abs(Vector3.Distance(this.transform.position, player.transform.position)) <= attackRange;
    }

    // Trigger behavior is off-loaded to the state. Write the method.
    public virtual void OnTriggerEnter (Collider other)
	{
        currentState.OnTriggerEnter(other);
	}

    /*
	 * bool SeesPlayer() looks for the player and returns whether or not the 
	 * player is spotted.
	 * It uses an offset vector if the chaseTarget isn't null, and just the 
	 * forward vector otherwise.
	 * This lets me combine otherwise duplicated code.  The analogous methods in 
	 * EnemyNoState are Look and LookWhileChasing.  If you look in the 
	 * original code, you should see the similarities.  
	 * You do not need to modify this method.
	 */
    internal virtual bool SeesPlayer()
    {
        RaycastHit hit;
        Vector3 lookVector = eyes.transform.forward;
        if (chaseTarget != null)
        {
            lookVector = (chaseTarget.position + offset) - eyes.transform.position;
        }
        if (Physics.Raycast(eyes.transform.position, lookVector, out hit, sightRange)
            && hit.collider.CompareTag("Player"))
        {
            // no need to search
            searchTimer = 0f;
            chaseTarget = hit.transform;
            lastSeenOrHeard = chaseTarget;
            return true;
        }
        else
        {
            chaseTarget = null;
            return false;
        }
        Debug.Log(hit.collider.CompareTag("Player"));
    }
	internal virtual void Patrol ()
	{
        // Set the correct way point to travel to
        navMeshAgent.destination = wayPoints[nextWayPoint].position;
        // Start traveling again
        navMeshAgent.isStopped = false;

        // Switch between way points when one is reached.
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
            !navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
        }

    }
    internal virtual void Chase()
    {
        navMeshAgent.destination = player.gameObject.transform.position;
        navMeshAgent.isStopped = false;
    }
    internal virtual bool SearchOver()
    {
        return searchTimer > searchingDuration;
    }

    internal virtual void Attack() {
        weapon.enemyAttack();
    }
    internal abstract void Search();
    internal abstract bool Alerted();
    public void Hit()
    {
        Destroy(this.gameObject);
        level.GuardKilled();
    }
}