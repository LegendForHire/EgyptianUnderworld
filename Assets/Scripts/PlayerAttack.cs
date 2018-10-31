using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	[SerializeField] private Arrow arrowPrefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Arrow arrow = Instantiate(arrowPrefab);
			arrow.transform.rotation = this.transform.rotation;
			arrow.transform.position = this.transform.TransformPoint(Vector3.forward * 1.5f);
			arrow.inMotion = true;
		}
	}
}
