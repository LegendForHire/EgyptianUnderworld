using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour {

    // The player dies on hitting lava
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SceneManager.LoadScene("DeathScreen");
        }
    }
}
