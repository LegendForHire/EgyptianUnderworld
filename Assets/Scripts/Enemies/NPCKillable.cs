using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCKillable : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
        if (col.gameObject.name.Contains("Arrow") || col.gameObject.name.Contains("Sword")) {
            SceneManager.LoadScene(this.gameObject.name + "End", LoadSceneMode.Single);
        }
	}
}
