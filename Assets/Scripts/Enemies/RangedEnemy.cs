using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
    internal override void Awake()
    {
        base.Awake();
        currentState = new PatrollingState(this);
        searchingDuration = 10f;
        sightRange = 80f;
        attackRange = 80f;
        attackRate = 1.2f;

    }

    internal override void Search()
    {
            
    }
    internal override bool Alerted()
    {
        return false;
    }
    //  started with code from here https://answers.unity.com/questions/1409312/how-do-i-make-my-enemy-look-at-player-on-only-one.html
    private void lookAtPlayer()
    {
        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = transform.position.y - .5f;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }
    class AlertedState : State
    {
        public AlertedState(Enemy enemy) : base(enemy)
        {
            nextHit = 0;
            enemy.sightRange = 80f;
        }

        public override void Update()
        {
            if (enemy.PlayerInRange()) enemy.currentState = new AttackState(enemy);
            if (enemy.SearchOver()) enemy.currentState = new PatrollingState(enemy);
            enemy.Search();
        }

    }
    class AttackState : State
    {
        RangedEnemy renemy;
        public AttackState(Enemy enemy) : base(enemy)
        {
            renemy = (RangedEnemy)enemy;
        }

        public override void Update()
        {
            renemy.lookAtPlayer();
            if (Time.time > nextHit && enemy.PlayerInRange())
            {
                nextHit = Time.time + enemy.attackRate;
                enemy.Attack();
            }
            if (!enemy.PlayerInRange()) enemy.currentState = new AlertedState(enemy);
        }

    }
    class PatrollingState : State
    {
        public PatrollingState(Enemy enemy) : base(enemy)
        {

            enemy.sightRange = 40f;
        }
        public override void Update()
        {
            if (enemy.Alerted()) enemy.currentState = new AlertedState(enemy);
            if (enemy.SeesPlayer()) enemy.currentState = new AttackState(enemy);
            enemy.Patrol();
        }
    }
}
