using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
    internal override void Awake()
    {
        base.Awake();
        currentState = new PatrollingState(this);
        searchingDuration = 10f;
        sightRange = 40f;
        attackRange = 80f;

    }
    internal override void Attack()
    {

    }
    internal override void Search()
    {

    }
    internal override bool Alerted()
    {
        return false;
    }
    class AlertedState : State
    {
        public AlertedState(Enemy enemy) : base(enemy)
        {
            enemy.sightRange = 80f;
        }

        public override void Update()
        {
            if (enemy.SeesPlayer()) enemy.currentState = new AttackState(enemy);
            if (enemy.SearchOver()) enemy.currentState = new PatrollingState(enemy);
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
            if (!enemy.PlayerInRange()) enemy.currentState = new AlertedState(enemy);
            enemy.Attack();
        }

    }
    class PatrollingState : State
    {
        public PatrollingState(Enemy enemy) : base(enemy)
        {
            if (enemy.Alerted()) enemy.currentState = new AlertedState(enemy);
            if (enemy.SeesPlayer()) enemy.currentState = new AttackState(enemy);
            enemy.sightRange = 40f;
        }
        public override void Update()
        {

            enemy.Patrol();
        }
    }
}
