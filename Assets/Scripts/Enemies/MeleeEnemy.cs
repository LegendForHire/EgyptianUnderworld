using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : Enemy
{
    private Vector3 searchHere;
    private float searchRadius = 2f;
    private float alertDistance = 5f;
    private float attackDamage = -.4f;
    private static float nextHit = 0;

    [SerializeField] private Image playerHealth;

    internal override void Awake()
    {
        base.Awake();
        currentState = new PatrollingState(this);
        searchingDuration = 10f;
        sightRange = 20f;
        attackRange = 3f;

    }
    internal override void Attack()
    {
        Debug.Log(playerHealth.fillAmount + attackDamage);
        playerHealth.fillAmount += attackDamage;
    }
    internal override void Search()
    {
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
    internal override bool Alerted()
    {
        lastSeenOrHeard = player.transform;
        return Vector3.Distance(player.transform.position, transform.position) < alertDistance;
    }
    class AlertedState : State
    {
        public AlertedState(MeleeEnemy enemy) : base(enemy)
        {
            enemy.searchTimer = 0;
            enemy.searchHere = enemy.player.transform.position;
            enemy.sightRange = 40f;
        }

        public override void Update()
        {
            if (enemy.SeesPlayer()) enemy.currentState = new ChasingState((MeleeEnemy)enemy);
            if (enemy.SearchOver()) enemy.currentState = new PatrollingState((MeleeEnemy)enemy);
            if (enemy.Alerted()) enemy.currentState = new AlertedState((MeleeEnemy)enemy);
            enemy.Search();
        }

    }
    class AttackState : State
    {
        public AttackState(MeleeEnemy enemy) : base(enemy)
        {
            enemy.searchHere = enemy.player.transform.position;
            enemy.attackRate = 1.5f;
        } 

        public override void Update()
        {
            if (Time.time > nextHit){
                nextHit = Time.time + enemy.attackRate;
                enemy.Attack();
            }
           
            if (!enemy.PlayerInRange()) enemy.currentState = new ChasingState((MeleeEnemy)enemy);
            if (!enemy.SeesPlayer()) enemy.currentState = new AlertedState((MeleeEnemy)enemy);

            
        }

    }
    class PatrollingState : State
    {
        public PatrollingState(MeleeEnemy enemy) : base(enemy)
        {
            enemy.sightRange = 20f;
        }

        public override void Update()
        {
            if (enemy.Alerted())
            {
                enemy.currentState = new AlertedState((MeleeEnemy)enemy);
            }
            if (enemy.SeesPlayer()) enemy.currentState = new ChasingState((MeleeEnemy)enemy);
            enemy.Patrol();
        }
    }

    class ChasingState : State
    {
        public ChasingState(MeleeEnemy enemy) : base(enemy)
        {
            enemy.searchHere = Vector3.negativeInfinity;
        }

        public override void Update()
        {
            enemy.Chase();

            if (!enemy.SeesPlayer())enemy.currentState= new AlertedState((MeleeEnemy)enemy);
            if (enemy.PlayerInRange()) enemy.currentState = new AttackState((MeleeEnemy)enemy);
        }
    }
}
