using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	public static PlayerHealth Instance;
	[SerializeField] private Image playerHealth;
	//how much health to regen
	private const float regenAmount = .0025f;
	//how often to regen the health
	private const float regenSpeed = .075f;
	// Use this for initialization
	void Awake() {
		Instance = this;
	}
	

	void Start () {
		// Call regen function repeatedly.
		InvokeRepeating("RegenHealth", 0, regenSpeed);
	}

	//update the image with the amount of damage
	public void TakeDamage(float damage){
		//make sure that the given damage won't break the slider
		if(damage>1 || damage<0){
			return;
		}
		playerHealth.fillAmount += (-damage);
        Debug.Log(playerHealth.fillAmount);
	}
	
	//update the slider with the amount of regened health
	void RegenHealth(){
		playerHealth.fillAmount += regenAmount;
	}

	// Update is called once per frame
	void Update () {
		// Determine if the player has died
        if (playerHealth.fillAmount <= 0) {
            SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
        }
	}
}
