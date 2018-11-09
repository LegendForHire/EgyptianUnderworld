using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

    public override void enemyAttack()
    {
      
    }

    public override void playerUse(Player player)
    {
        StartCoroutine(SwingSword());
    }
    public IEnumerator SwingSword(){
        yield return new WaitForSeconds(1);
        float i = 0;
        Vector3 swingAngle = new Vector3(-85, 0, 110);
        Vector3 swingAngle2 = new Vector3(-40, 0, 65);
        while (i < 1){
            i += .01f;
            Vector3 swordPos = Vector3.negativeInfinity;
            if (i < .5) transform.localEulerAngles = Vector3.Lerp(swingAngle, swingAngle2, i*2);
        }

        
    }
    // Used from here
    public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float r = 1f - t;
        float f0 = r * r * r;
        float f1 = r * r * t * 3;
        float f2 = r * t * t * 3;
        float f3 = t * t * t;
        return f0 * p0 + f1 * p1 + f2 * p2 + f3 * p3;
    }
}
