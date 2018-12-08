using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Attack {
	public float speed = 40F;
	public bool inMotion = false;

    private int arrowLife = 10;

	// Use this for initialization
	public override void Start () {
        StartCoroutine(DestroyAfter(arrowLife));
	}

	// Update is called once per frame
	public override void Update () {
		if (!inMotion) {
			return;
		}

		this.transform.position += transform.forward * Time.deltaTime * speed;
	}

	public override void OnCollisionEnter(Collision collision) {
        inMotion = false;
        Destroy(this.GetComponent<Rigidbody>());
        foreach (BoxCollider bc in this.GetComponentsInChildren<BoxCollider>())
        {
            Destroy(bc);
        }
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hit();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name.Contains("Player"))
        {
            player.Hit(this);
            Destroy(this.gameObject);
        }
    }
    public override float GetDamage()
    {
        return speed / 250;
    }
    private IEnumerator DestroyAfter(int time) {
		yield return new WaitForSeconds(time);
		Destroy(this.gameObject);
	}
}
