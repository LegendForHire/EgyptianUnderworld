using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager: MonoBehaviour{
    public delegate void NoInputAction();

    private Dictionary<string, NoInputAction> keyActions;
    private List<NoInputAction> mouseActions;

    public void Awake(){
        keyActions = new Dictionary<string, NoInputAction>();
        mouseActions = new List<NoInputAction>();
    }

    public void RegisterKey(string keyCode, NoInputAction actionFunction){
        //Debug.Log("keyActions is null? ");
        //Debug.Log(""+keyActions == null);
        keyActions.Add(keyCode, actionFunction);
    }

    public void RegisterMouseButton(NoInputAction actionFunction)
    {
        mouseActions.Add(actionFunction);
    }

    public void Update(){
        foreach(KeyValuePair<string, NoInputAction> pair in keyActions){
            if (Input.GetKeyDown(pair.Key))
            {
                //Debug.Log("Received key press: "+pair.Key);
                pair.Value();
            }
        }
        foreach(NoInputAction action in mouseActions){
            if(Input.GetMouseButtonDown(0)){
                action();
            }
        }
    }
}
