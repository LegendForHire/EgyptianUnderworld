using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
	[SerializeField] private VideoPlayer intro;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!intro.isPlaying) {
			SceneManager.LoadScene("TownCenter", LoadSceneMode.Single);
		}
	}
}
