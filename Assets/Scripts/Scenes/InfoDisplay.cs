using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *   Written by Ryan Kugel
 */
public class InfoDisplay : MonoBehaviour {
    public List<string> textList;

    private Button nextButton;
    [SerializeField] private Text info;
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

    public int currentItem = 0;

	// Use this for initialization
	void Start () {
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(NextClick);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (textList == null) return;

        // show the info at the current item
        info.text = textList[currentItem];

        // don't let the player move while this is open
        firstPersonController.canMove = false;
	}

    // Open this display
    public void OpenDisplay() {
        nextButton.GetComponentInChildren<Text>().text = "Next";
        info.text = textList[currentItem];
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.gameObject.SetActive(true);
    }

    // Close this display
    private void CloseDisplay() {
        firstPersonController.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.SetActive(false);
    }

    // Handle next button clicking
    private void NextClick() {

        // hide the display if we've reached the last bit of text
        if (IsLastItem()) {
            CloseDisplay();
            return;
        } 

        // increment the current item
        currentItem++;

        // if we're now at the last item, change button text to close
        if (IsLastItem()) {
            nextButton.GetComponentInChildren<Text>().text = "Close";
        }
    }

    private bool IsLastItem() {
        return currentItem == textList.Count - 1;
    }

}
