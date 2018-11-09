using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public float speed = 20f;
	public bool inMotion = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyAfter20());
	}

	// Update is called once per frame
	void Update () {
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

	private IEnumerator DestroyAfter20() {
		yield return new WaitForSeconds(20);
		Destroy(this.gameObject);
	}
}
