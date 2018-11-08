using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
	private ILevel level;

	// Use this for initialization
	void Start () {
		level = GameObject.Find("Level").GetComponent<ILevel>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log("Here bitch");

		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "SceneLoader") {
			Debug.Log ("Got sceneloader");
			if (level.ObjectivesComplete()) {
				Debug.Log("Level complete");
				SceneManager.LoadScene(other.gameObject.tag, LoadSceneMode.Single);
			}
		}
	}
}
