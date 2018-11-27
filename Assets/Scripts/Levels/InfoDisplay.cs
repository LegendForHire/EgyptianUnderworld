using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour {
    public List<string> textList;

    private Button nextButton;
    [SerializeField] private Text info;
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

    private int currentItem = 0;

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

    private void NextClick() {

        // hide the display if we've reached the last bit of text
        if (IsLastItem()) {
            firstPersonController.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
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
