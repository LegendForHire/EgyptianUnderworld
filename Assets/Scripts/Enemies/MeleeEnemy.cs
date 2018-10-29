using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {
    [SerializeField] private Vector3[] patrolPoints;
    public MeleeEnemy() : base()
    {
        currentState = new PatrolState(this);
    }
	// Use this for initialization
    override boolean isAlerted()
    {
        return false;
    }
    override boolean spottedPlayer()
    {
        return false;
    }
    override boolean playerInRange()
    {
        return false;
    }
    class AlertedState : State
    {
        public AlertedState(MeleeEnemy enemy) : base(enemy)
        {

        }
        override void Update()
        {
            if (Enemy.alertOver()) enemy.currentState = new PatrolState(enemy); ;
            if (enemy.spottedPlayer())
            {
                if (enemy.playerInRange()) enemy.currentState = new AttackState(enemy);
                else enemy.currentState = new ChaseState(enemy);
            }
        }
    }
    class PatrolState : State
    {
        public PatrolState(MeleeEnemy enemy) : base(enemy)
        {

        }
        override void Update()
        {
            if (enemy.isAlerted()) enemy.currentState = new AlertedState(enemy);
        }
    }
    class ChaseState : State
    {
        public ChaseState(MeleeEnemy enemy) : base(enemy)
        {

        }
        override void Update()
        {
            if (enemy.playerInRange) enemy.currentState = new AttackState(enemy);
            if (!enemy.playerSpotted) enemy.currentState = new AlertedState(enemy);
        }
    }
    abstract class AttackState : State
    {
        public AttackState(MeleeEnemy enemy) : base(enemy)
        {

        }
        virtual override void Update()
        {
            if (!enemy.playerInRange) enemy.currentState = new ChaseState(enemy);
            if (!enemy.playerSpotted) enemy.currentState = new AlertedState(enemy);
        }
    }
}
