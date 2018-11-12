using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    public override void enemyAttack()
    {
      
    }
    public override void playerUse(Player player)
    {
        SwordSwing ss = gameObject.AddComponent(typeof(SwordSwing)) as SwordSwing;
        StartCoroutine(DestroyAfterTime(.33f, ss));
        
    }
    private IEnumerator DestroyAfterTime(float t, Behaviour b)
    {
        yield return new WaitForSeconds(t);
        Destroy(b);
        transform.localPosition = new Vector3(.8f, -.2f, 1);
        transform.localEulerAngles = new Vector3(-90, 120, 0);
    }
}
