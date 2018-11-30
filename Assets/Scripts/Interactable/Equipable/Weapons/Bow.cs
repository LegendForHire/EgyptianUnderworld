using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Arrow arrowPrefab;

    public override void enemyAttack(Enemy enemy) {
        player = enemy.player;
        Vector3 lookVector = enemy.transform.forward;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        Fire(40f, rot);
        //drawBow(rot);
    }

    public override void playerUse(Player player) {
        Vector3 lookVector = player.transform.forward;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        Fire(40f, rot);
        //drawBow(rot);
    }

    IEnumerator drawBow(Quaternion rot)
    {
        bool mouseRealeased = false;
        float start = Time.time;
        yield return new WaitUntil(() => mouseRealeased);
        Debug.Log(Time.time - start);
        Fire(40f, rot);

    }

    // Instantiate an arrow and shoot it
    void Fire(float speed, Quaternion rot)
    {
        Arrow arrow = Instantiate(arrowPrefab);
        arrow.player = player;
        arrow.transform.rotation = rot;
        arrow.transform.position = this.transform.parent.transform.TransformPoint(Vector3.forward * 1.5f);
        Vector3 pos = arrow.transform.position;
        arrow.transform.position = new Vector3(pos.x, pos.y - .1f, pos.z);
        arrow.inMotion = true;
        arrow.speed = speed;
    }
}
