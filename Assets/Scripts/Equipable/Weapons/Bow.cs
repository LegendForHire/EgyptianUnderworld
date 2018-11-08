using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Arrow arrowPrefab;

    public override void enemyAttack()
    {

    }

    public override void playerUse(Player player)
    {
        Arrow arrow = Instantiate(arrowPrefab);
        arrow.transform.rotation = this.transform.rotation;
        arrow.transform.position = this.transform.TransformPoint(Vector3.forward * 1.5f);
        arrow.inMotion = true;
    }
}
