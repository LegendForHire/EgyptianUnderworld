using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : Enemy {

    private Vector3 searchHere;
    private float searchRadius = 2f;
    private float alertDistance = 5f;
    private float attackDamage = .4f;
    private static float nextHit = 0;


    internal override void Awake() {
        base.Awake();
        currentState = new PatrollingState(this);
        searchingDuration = 10f;
        sightRange = 20f;
        attackRange = 2.5f;
        attackRate = .5f;


    }

    internal override void Search() {
        searchTimer += Time.deltaTime;
        navMeshAgent.destination = searchHere;
        navMeshAgent.isStopped = false;

        if ((navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
            !navMeshAgent.pathPending) || !navMeshAgent.hasPath)
        {
            Vector3 lastPos = lastSeenOrHeard.transform.position;
            searchHere = new Vector3(lastPos.x + Random.value * searchRadius * searchTimer, 1.2f, lastPos.z * searchRadius * searchTimer);
        }

    }

    internal override bool Alerted() {
        lastSeenOrHeard = player.transform;
        return Vector3.Distance(player.transform.position, transform.position) < alertDistance;
    }


    // Enemy alerted state, search for the player
    class AlertedState : State {
        public AlertedState(MeleeEnemy enemy) : base(enemy) {
            Debug.Log("Alerted");
            enemy.searchTimer = 0;
            enemy.searchHere = enemy.player.transform.position;
            enemy.sightRange = 40f;
        }

        public override void Update() {
            if (enemy.SeesPlayer()) enemy.currentState = new ChasingState((MeleeEnemy)enemy);
            if (enemy.SearchOver()) enemy.currentState = new PatrollingState((MeleeEnemy)enemy);
            if (enemy.Alerted()) enemy.currentState = new AlertedState((MeleeEnemy)enemy);
            enemy.Search();
        }

    }


    // Base state for the MeleeEnemy, just patrol and look for the player
    class PatrollingState : State {
        public PatrollingState(MeleeEnemy enemy) : base(enemy) {
            Debug.Log("patrol");
            enemy.sightRange = 30;
        }

        public override void Update() {
            // never leave the patrolling state if the player is unarmed
            if (!enemy.player.HasWeapon()) {
                enemy.Patrol();
                return;
            }

            if (enemy.Alerted()) {
                enemy.currentState = new AlertedState((MeleeEnemy)enemy);
            }
            if (enemy.SeesPlayer()) enemy.currentState = new ChasingState((MeleeEnemy)enemy);
            enemy.Patrol();
        }
    }

    // Chasing player state, either attack or go alert after chase
    class ChasingState : State {
        public ChasingState(MeleeEnemy enemy) : base(enemy) {
            Debug.Log("Chase");
            enemy.searchHere = Vector3.negativeInfinity;
            nextHit = 0;
        }

        public override void Update() {
            enemy.Chase();
            
            if (Time.time > nextHit && enemy.PlayerInRange())
            {
                nextHit = Time.time + enemy.attackRate;
                enemy.Attack();
            }
            if (!enemy.SeesPlayer() && !enemy.PlayerInRange()) enemy.currentState= new AlertedState((MeleeEnemy)enemy);
        }
    }
}
