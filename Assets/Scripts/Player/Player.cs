using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera camera;
    private Equipable equipped;
    private float equipRange = 5f;
    private GameObject equipable;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        equipable = null;
        equipped = null;
    }
	
	// Update is called once per frame
	void Update () {
        OnSeesEquipable();
	}
    void OnSeesEquipable(){
        RaycastHit hit;
        Vector3 lookVector = camera.transform.forward;
        if ((Physics.Raycast(camera.transform.position, lookVector, out hit, equipRange)
            && hit.collider.gameObject.GetComponent<Equipable>() != null))
        {
            equipable = hit.collider.gameObject;
            Behaviour halo = (UnityEngine.Behaviour)equipable.GetComponent("Halo");
            if (halo != null) halo.enabled = false;

        }
        else{
            if(equipable != null){
                Behaviour halo = (UnityEngine.Behaviour)equipable.GetComponent("Halo");
                if(halo!=null)halo.enabled = false;
            }
            equipable = null;
        }
    }
}
