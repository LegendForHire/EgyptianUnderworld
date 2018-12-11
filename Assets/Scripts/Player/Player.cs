using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour
{
    [SerializeField] private PostProcessingProfile post;
    public bool canUse = true;
    public Equipable equipped;
    protected float interactRange = 6f;
    protected Interactable interactable;
    public Vector3 playerVelocity = Vector3.zero;
    private Vector3 lastPosition = Vector3.zero;
    [SerializeField] private InputManager inputs;
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBody;
    private PlayerState state;
    private Vector3 bodyLocation;

    [SerializeField] private Equipable bow;
    [SerializeField] private Equipable sword;

    // Use this for initialization
    void Start()
    {
        state = new NormalState(this);
        interactable = null;
        inputs.RegisterKey("f", Interact);
        inputs.RegisterKey("q", OutOfBody);
        inputs.RegisterMouseButton(Use);

        // Get player's weapon from player prefs
        if (PlayerPrefs.HasKey("playerWeapon") && bow != null && sword != null) {
            Equipable[] weapons = { bow, sword };

            string weapon = PlayerPrefs.GetString("playerWeapon");
            for (int i = 0; i < weapons.Length; i++) {
                if (weapons[i].gameObject.name == weapon) {
                    Equip(weapons[i]);
                    weapons[i].equipped = true;
                    weapons[i].player = this;
                    break;
                }
            }
        } else {
            equipped = null;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        OnSeesInteractable();
    }
    void FixedUpdate()
    {
        playerVelocity = transform.position - lastPosition;
        lastPosition = transform.position;

    }
    // Create halo glow on interactable object the player sees
    void OnSeesInteractable()
    {
        state.OnSeesInteractable();
    }
    public void Interact()
    {
        state.Interact();
    }

    public void Use()
    {
        state.Use();
    }

    // Pick up a weapon
    public void Equip(Equipable equipment)
    {
        if (equipped != null)
        {
            equipped.transform.parent = transform.parent.transform.parent;
            equipped.equipped = false;
        }

        equipment.transform.parent = transform;
        equipped = equipment;
        equipped.gameObject.transform.localPosition = new Vector3(.8f, -.2f, 1);
        equipped.transform.localEulerAngles = new Vector3(-90, 120, 0);

        // Store weapon name in player prefs for persistence
        PlayerPrefs.SetString("playerWeapon", equipment.gameObject.name);
    }

    // Returns true if the player has a weapon equipped
    public bool HasWeapon()
    {
        return (equipped != null);
    }
    public void Hit(Attack a)
    {
        //Debug.Log("hitting");
        PlayerHealth.Instance.TakeDamage(a.GetDamage());

    }
    public void OutOfBody()
    {
        bodyLocation = transform.parent.position;
        playerBody.transform.parent = transform.parent.transform.parent;
        state = new OutOfBodyState(this);
        playerBody.transform.rotation = new Quaternion(0, 0, 0, 0);

    }
    public Vector3 getBodyLocation()
    {
        return playerBody.transform.position;
    }

    public abstract class PlayerState
    {
        protected Player player;
        public PlayerState(Player p)
        {
            player = p;

        }

        public virtual void OnSeesInteractable()
        {
            RaycastHit hit;
            Vector3 lookVector = player.transform.forward;
            Vector3 pos = player.transform.position;
            // interactable, non-equipable objects
            if (Physics.Raycast(pos, lookVector, out hit, player.interactRange))
            {
                if (hit.collider.gameObject.tag == "ObjectInteract")
                {
                    player.interactable = hit.collider.gameObject.GetComponent<Interactable>();
                    Behaviour halo = (UnityEngine.Behaviour)player.interactable.GetComponent("Halo");

                    if (halo != null && player.interactable.isInteractable()) halo.enabled = true;
                    else player.interactable = null;
                    return;
                }


               if ((Physics.Raycast(pos, lookVector, out hit, player.interactRange)
                    && hit.collider.gameObject.GetComponent<Equipable>() != null))
                {

                    player.interactable = hit.collider.gameObject.GetComponent<Equipable>();
                    Behaviour halo = (UnityEngine.Behaviour)player.interactable.GetComponent("Halo");
                    if (halo != null && player.interactable.isInteractable()) halo.enabled = true;
                    else player.interactable = null;

                }
                else
                {
                    if (player.interactable != null)
                    {
                        Behaviour halo = (UnityEngine.Behaviour)player.interactable.GetComponent("Halo");
                        if (halo != null) halo.enabled = false;
                    }
                    player.interactable = null;
                }
            }
        }
        public virtual void Interact()
        {
            if(player.interactable != null)player.interactable.Interact(player);
        }
        public virtual void Use(){
            if (player.equipped != null && player.canUse) player.equipped.playerUse(player);
        }
    }
    public class NormalState : PlayerState
    {
        public NormalState(Player p) : base(p)
        {

        }
    }
    public class OutOfBodyState : PlayerState
    {
        public OutOfBodyState(Player p) : base(p)
        {
            // Change post processing on out of body
            var colorSettings = player.post.colorGrading.settings;
            colorSettings.basic.saturation = 0f;
            player.post.colorGrading.settings = colorSettings;

            p.StartCoroutine(ReturnToBodyIn(10));
        }

        private IEnumerator ReturnToBodyIn(int v)
        {
            yield return new WaitForSeconds(10);
            if(player.playerBody.transform.parent != player.transform)
            {
                returnToBody();
            }
        }
        public override void OnSeesInteractable()
        {
            RaycastHit hit;
            Vector3 lookVector = player.transform.forward;
            Vector3 pos = player.transform.position;
            Behaviour halo = (UnityEngine.Behaviour)player.playerBody.GetComponent("Halo");
            if (Physics.Raycast(pos, lookVector, out hit, player.interactRange) && hit.collider.gameObject.tag == "Player")
                halo.enabled = true;
            else halo.enabled = false;


        }
        public override void Interact()
        {
            Behaviour halo = (UnityEngine.Behaviour)player.playerBody.GetComponent("Halo");
            if (halo.enabled)
            {
                returnToBody();
            }
        }
        public void returnToBody(){
            player.state = new NormalState(player);
            player.transform.parent.position = player.getBodyLocation();
            player.playerBody.transform.parent = player.transform;

            // Update color settings
            var colorSettings = player.post.colorGrading.settings;
            colorSettings.basic.saturation = 1f;
            player.post.colorGrading.settings = colorSettings;
        }
        public override void Use(){}

    }
}

