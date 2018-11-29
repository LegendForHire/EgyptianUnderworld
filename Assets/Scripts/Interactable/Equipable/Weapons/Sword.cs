using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    SwordSwing ss;
    Player player;
    public override void Update()
    {
        if (ss == null && equipped)
        {
            transform.localPosition = new Vector3(.8f, -.2f, 1);
            transform.localEulerAngles = new Vector3(-90, 120, 0);
            tag = "Untagged";
            Destroy(GetComponent<Rigidbody>());
        }
    }
    public override void enemyAttack(Enemy enemy)
    {
        gameObject.AddComponent<Rigidbody>();
        ss = gameObject.AddComponent(typeof(SwordSwing)) as SwordSwing;
        ss.player = player;
        StartCoroutine(DestroyAfterTime(.33f, ss));

    }
    public override void playerUse(Player player)
    {
        if (ss == null)
        {
            gameObject.AddComponent<Rigidbody>();
            ss = gameObject.AddComponent(typeof(SwordSwing)) as SwordSwing;
            StartCoroutine(DestroyAfterTime(.33f, ss));
        }     
    }
    private IEnumerator DestroyAfterTime(float t, Behaviour b)
    {
        yield return new WaitForSeconds(t);
        if (Input.GetMouseButtonDown(0)) {
            ss = gameObject.AddComponent(typeof(SwordSwing2)) as SwordSwing2;
            StartCoroutine(DestroyAfterTime(.33f, ss));
        }
        else
        {
            ss = null;
        }
        Destroy(b);
    }
}
