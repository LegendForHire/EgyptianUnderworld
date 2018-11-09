using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Equipable equipped;
    private float equipRange = 4f;
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
            
            equipable = hit.collider.gameObject.GetComponent<Equipable>();
            Behaviour halo = (UnityEngine.Behaviour)equipable.GetComponent("Halo");
            if (halo != null) halo.enabled = true;

        }
        else
        {
            if (equipable != null)
            {
                Behaviour halo = (UnityEngine.Behaviour)equipable.GetComponent("Halo");
                if (halo != null) halo.enabled = false;
            }
            equipable = null;

        }
    }
    public void Equip(){
        if (equipable != null){
            equipable.transform.parent = transform;
            equipped = equipable;
            equipped.gameObject.transform.localPosition = new Vector3(.8f, -.2f, 1);
            equipped.transform.localEulerAngles = new Vector3(90,120,0);

        }
    }
    public void Use(){
        if (equipped != null)equipped.playerUse(this);
    }

}
