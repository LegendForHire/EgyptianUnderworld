using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Level : MonoBehaviour {
    [SerializeField] protected InfoDisplay infoDisplay;
    [SerializeField] protected PauseMenu pauseMenu;
    [SerializeField] protected List<Objective> objectives;
    [SerializeField] protected List<GameObject> hudObjects;
    [SerializeField] protected Text objectiveText;
    [SerializeField] protected Player player;

    protected List<string> infoText;
    protected Objective currentObjective;
    protected int objectiveIndex = 0;
    protected bool pauseOpen = false;

    protected virtual void Start() {
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update() {
        ShowHUD(!infoDisplay.isActiveAndEnabled);

        // Check for escape key press
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseMenu.gameObject.activeSelf) {
                pauseMenu.CloseDialog();
            } else {
                pauseMenu.OpenDialog();
            }
        }

        // Advance objectives
        if (currentObjective.IsComplete() && objectiveIndex != objectives.Count-1) {
            objectiveIndex++;
            currentObjective = objectives[objectiveIndex];
            objectiveText.text = currentObjective.GetDescription();
        }
    }

    // Show or hide the HUD
    internal virtual void ShowHUD(bool visible) {
        for (int i = 0; i < hudObjects.Count; i++) {
            hudObjects[i].SetActive(visible);
            player.canUse = visible;
        }
    }

    // Set the end level results
    internal virtual void SetLevelResults() {
        int success = 1;
        string resultsText = "";

        for (int i = 0; i < objectives.Count; i++) {
            if(!objectives[i].IsComplete()) {
                success = 0;
                resultsText += objectives[i].GetFailureText();
            }
        }
        PlayerPrefs.SetInt("success", success);

        resultsText += "Let's move on...";
        PlayerPrefs.SetString("resultsText", resultsText);
    }
}
