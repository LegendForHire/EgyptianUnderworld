using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {


	[SerializeField] private Image enemyHealthBackground;
	[SerializeField] private Image enemyHealthBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = Camera.main.WorldToScreenPoint (transform.position);
		enemyHealthBackground.transform.position = targetPos;
		enemyHealthBar.transform.position = targetPos;
	}
}
