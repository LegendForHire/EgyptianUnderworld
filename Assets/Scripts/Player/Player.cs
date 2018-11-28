using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Equipable equipped;
    private float interactRange = 6f;
    private Interactable interactable;
    [SerializeField] private InputManager inputs;

    private ILevel level;

    // Use this for initialization
    void Start () {
        interactable = null;
        equipped = null;
        inputs.RegisterKey("f", Interact);
        inputs.RegisterMouseButton(Use);
        level = GameObject.Find("Level").GetComponent<ILevel>();
    }
	
	// Update is called once per frame
	void Update () {
        OnSeesInteractable();
	}

    // Create halo glow on interactable object the player sees
    void OnSeesInteractable() {
        RaycastHit hit;
        Vector3 lookVector = transform.forward;
        Vector3 pos = transform.position;

        // interactable, non-equipable objects
        if (Physics.Raycast(pos, lookVector, out hit, interactRange)) {
            if (hit.collider.gameObject.tag == "ObjectInteract") {
                interactable = hit.collider.gameObject.GetComponent<Interactable>();
                Behaviour halo = (UnityEngine.Behaviour)interactable.GetComponent("Halo");
                if (halo != null) halo.enabled = true;
                return;
            }
        }

        if ((Physics.Raycast(pos, lookVector, out hit, interactRange)
            && hit.collider.gameObject.GetComponent<Equipable>() != null))
        {

            interactable = hit.collider.gameObject.GetComponent<Equipable>();
            Behaviour halo = (UnityEngine.Behaviour)interactable.GetComponent("Halo");
            if (halo != null) halo.enabled = true;

        }
        else {
            if (interactable != null) {
                Behaviour halo = (UnityEngine.Behaviour)interactable.GetComponent("Halo");
                if (halo != null) halo.enabled = false;
            }
            interactable = null;
        }
    }
    public void Interact()
    {
        interactable.Interact(this);
    }

    // Pick up a weapon
    public void Equip(Equipable equipment) {
            if (equipped != null) {
                equipped.transform.parent = transform.parent.transform.parent;
                equipped.equipped = false;
            }
            equipment.transform.parent = transform;
            equipped = equipment;
            equipped.gameObject.transform.localPosition = new Vector3(.8f, -.2f, 1);
            equipped.transform.localEulerAngles = new Vector3(-90,120,0);
            level.GotWeapon();
    }

    public void Use() {
        if (equipped != null)equipped.playerUse(this);
    }

    // Returns true if the player has a weapon equipped
    public bool HasWeapon() {
        return (equipped != null);
    }
    public void Hit(Attack a)
    {
        //Debug.Log("hitting");
        PlayerHealth.Instance.TakeDamage(a.GetDamage());
    }
}
