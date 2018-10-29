using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public RangedEnemy() : base()
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
    class AleretedState : State
    {
        public AlertedState(RangedEnemy enemy) : base(enemy)
        {

        }
        override void Update()
        {
            if (Enemy.alertOver()) enemy.currentState = new PatrolState(enemy);
            if (enemy.spottedPlayer() && enemy.playerInRange()) enemy.currentState = new AttackState(enemy); ;

        }
    }
    class PatrolState : State
    {
        public PatrolState(RangedEnemy enemy) : base(enemy)
        {

        }
        override void Update()
        {
            if (enemy.isAlerted()) enemy.currentState = new AlertedState(enemy);
        }
    }
    abstract class AttackState : State
    {
        public AttackState(RangedEnemy enemy) : base(enemy)
        {

        }
        virtual override void Update()
        {
            if (!enemy.playerInRange || !enemy.playerSpotted) enemy.currentState = new AlertedState(enemy);
        }
    }
}