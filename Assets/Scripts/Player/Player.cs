using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Equipable equipped;
    private float equipRange = 3f;
    private Equipable equipable;
    [SerializeField] private InputManager inputs;

    // Use this for initialization
    void Start (){
        equipable = null;
        equipped = null;
        inputs.RegisterKey("f", Equip);
        inputs.RegisterMouseButton(Use);
    }
	
	// Update is called once per frame
	void Update () {
        OnSeesEquipable();
	}
    void OnSeesEquipable(){
        RaycastHit hit;
        Vector3 lookVector = transform.forward;
        Vector3 pos = transform.position;
        if ((Physics.Raycast(pos, lookVector, out hit, equipRange)
            && hit.collider.gameObject.GetComponent<Equipable>() != null))
        {
            Debug.Log("sees equip");
            equipable = hit.collider.gameObject.GetComponent<Equipable>();
            Behaviour halo = (UnityEngine.Behaviour)equipable.GetComponent("Halo");
            if (halo != null) halo.enabled = true;

        }
        else{
           
            if (equipable != null){
                Behaviour halo = (UnityEngine.Behaviour)equipable.GetComponent("Halo");
                if(halo!=null)halo.enabled = false;
            }
            equipable = null;
        }
    }
    public void Equip(){
        if (equipable != null){
            equipable.transform.parent = transform.parent;
            equipped = equipable;
            Vector3 thisPos = transform.position;
            Vector3 pos = new Vector3(thisPos.z + -1f, thisPos.y, thisPos.z + 1f);
            equipped.gameObject.transform.position = pos;
        }
    }
    public void Use(){
        if (equipped != null)equipped.playerUse(this);
    }
}
