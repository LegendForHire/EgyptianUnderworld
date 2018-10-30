using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {


    internal override void Awake(){
        base.Awake();
        currentState = new PatrollingState(this);
        searchingDuration = 10f;
        sightRange = 20f;
        attackRange = 5f;

	}
    internal override void Attack()
    {

    }
    internal override void Search()
    {

    }
    class AlertedState : State
    {
        public AlertedState(Enemy enemy) : base(enemy)
        {
            enemy.sightRange = 40f;
        }

        public override void Update()
        {
            enemy.Search();
        }

    }
    class AttackState : State
    {
        public AttackState(Enemy enemy) : base(enemy)
        {

        } 

        public override void Update()
        {
            enemy.Attack();
        }

    }
    class PatrollingState : State
    {
        public PatrollingState(Enemy enemy) : base(enemy)
        {
            enemy.sightRange = 20f;
        }

        public override void Update()
        {
            enemy.Patrol();
        }
    }

    class ChasingState : State
    {
        public ChasingState(Enemy enemy) : base(enemy)
        {

        }

        public override void Update()
        {
            enemy.Chase();
            if (!enemy.SeesPlayer())enemy.currentState= new AlertedState(enemy);
            if (enemy.PlayerInRange()) enemy.currentState = new AttackState(enemy);
        }
    }
}
