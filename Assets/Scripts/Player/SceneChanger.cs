using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
	private ILevel level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Load scene according to this SceneChanger's tag
	void OnTriggerEnter(Collider other) {
		level = GameObject.Find("Level").GetComponent<ILevel>();

		if (other.gameObject.name == "SceneLoader") {
            // Level was completed
            if (other.gameObject.tag == "LevelEnd") {
                if (level.ObjectivesComplete()) {
                    level.SetLevelResults();
                    SceneManager.LoadScene(other.gameObject.tag, LoadSceneMode.Single);
                    return;
                } else {
                	return;
                }
            }

            SceneManager.LoadScene(other.gameObject.tag, LoadSceneMode.Single);
        }
	}
}
