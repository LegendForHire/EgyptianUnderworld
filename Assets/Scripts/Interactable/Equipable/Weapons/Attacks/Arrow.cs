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

	private void OnCollisionEnter(Collision collision) {
        inMotion = false;
        Destroy(this.GetComponent<Rigidbody>());
        if (collision.gameObject.name.Contains("Enemy")) Destroy(this.gameObject);
	}

	private IEnumerator DestroyAfter(int time) {
		yield return new WaitForSeconds(time);
		Destroy(this.gameObject);
	}
}
