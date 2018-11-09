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

    // Load scene according to this SceneChanger's tag
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "SceneLoader") {
			if (level.ObjectivesComplete()) {
				SceneManager.LoadScene(other.gameObject.tag, LoadSceneMode.Single);
			}
		}
	}
}
