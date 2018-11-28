using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : Attack {
    protected float t;
	// Use this for initialization
	public override void Start() {
        t = 0;
        tag = "Weapon";
	}
	
	// Update is called once per frame
	public override void Update () {
        t += Time.deltaTime*3;
        Vector3 swingAngle = new Vector3(-85, -40, 110);
        Vector3 swingAngle2 = new Vector3(-40, -40, 65);
        Swing();
        transform.localEulerAngles = cubeBezier3(swingAngle, swingAngle, swingAngle2, swingAngle2, t);
    }
    protected virtual void Swing()
    {
        Vector3 swingPos = new Vector3(1, 1, 1f);
        Vector3 swingPos2 = new Vector3(.33f, .33f, 1.75f);
        Vector3 swingPos3 = new Vector3(-.33f, -.33f, 1.75f);
        Vector3 swingPos4 = new Vector3(-1, -1, 1f);
        transform.localPosition = cubeBezier3(swingPos, swingPos2, swingPos3, swingPos4, t);
        
    }
    // Used from here https://answers.unity.com/questions/959091/how-can-i-make-a-lerp-move-in-an-arc-instead-of-a.html
    public static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float r = 1f - t;
        float f0 = r * r * r;
        float f1 = r * r * t * 3;
        float f2 = r * t * t * 3;
        float f3 = t * t * t;
        return f0 * p0 + f1 * p1 + f2 * p2 + f3 * p3;
    }
    public override void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hit();
        }
        if (collision.gameObject.name.Contains("PlayerBody"))
        {
            Player player = collision.transform.parent.gameObject.GetComponent<Player>();
            player.Hit(this);
        }
    }
    public override float GetDamage()
    {
        return .4f;
    }
}
