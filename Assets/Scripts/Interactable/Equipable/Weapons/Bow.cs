using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Arrow arrowPrefab;

    public override void enemyAttack() {
        //drawbow();
        Arrow arrow = Instantiate(arrowPrefab);
        Vector3 rotation = transform.eulerAngles;
        arrow.transform.localEulerAngles = new Vector3(rotation.x - 90f, rotation.y , rotation.y);
        arrow.transform.position = this.transform.parent.transform.TransformPoint(Vector3.forward * 1.5f);
        Vector3 pos = arrow.transform.position;
        arrow.transform.position = new Vector3(pos.x, pos.y - .1f, pos.z);
        arrow.inMotion = true;
    }

    public override void playerUse(Player player) {
        //drawBow();
        Arrow arrow = Instantiate(arrowPrefab);
        Vector3 rotation = transform.eulerAngles;
        arrow.transform.localEulerAngles = new Vector3(rotation.x - 90f, rotation.y, rotation.y);
        arrow.transform.position = this.transform.parent.transform.TransformPoint(Vector3.forward * 1.5f);
        Vector3 pos = arrow.transform.position;
        arrow.transform.position = new Vector3(pos.x, pos.y - .1f, pos.z);
        arrow.inMotion = true;
    }

    IEnumerator drawBow() {
        bool mouseRealeased = false;
        yield return new WaitUntil(() => !mouseRealeased);
        Arrow arrow = Instantiate(arrowPrefab);
        Vector3 Hrotation = transform.parent.transform.localEulerAngles;
        Vector3 Vrotation = transform.parent.transform.parent.transform.localEulerAngles;
        arrow.transform.localEulerAngles = new Vector3(Hrotation.x, Vrotation.y, Hrotation.y);
        arrow.transform.position = this.transform.parent.transform.TransformPoint(Vector3.forward * 1.5f);
        Vector3 pos = arrow.transform.position;
        arrow.transform.position = new Vector3(pos.x, pos.y - .1f, pos.z);
        arrow.inMotion = true;

    }

    // Instantiate an arrow and shoot it
    void Fire(float speed) {
        
        //arrow.speed = speed;
    }
}
