using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerScript, interfaces with the user to maintain control over the player. Specifically its sprite, animations, in-game actions, and stats.
// Main programmer: Thomas Pickering
public class PlayerScript : MonoBehaviour
{
    /* Editor-changeable fields
     * Anything with SerializeField can be modified in the editor.
     * Those changes will only last for as long as the script does not change it again.
     * 
     * 
     * Note to artists: When changing animations, make sure all names and references remained unchanged. 
     * If adding any new animations and help is needed to integrate it into the Player entity, consult one of the programmers.
     *
     * Note to programmers: Any component added via the editor should be declared as a field (not serialized, since that is on by default) and initialized in the Start() function.
     * 
     * -Thomas Pickering
     */

    [SerializeField] private Animator p_anim_controller;
    [SerializeField] private InventorySystem p_inventory_handler;
    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject spell;
    [SerializeField] private float castCooldown;
    [SerializeField] private GameObject aimingLineContainer;
    [SerializeField] public GameObject keybindobject;

    KeyBindScript keybind;

    GlobalVars instance;

    private LineRenderer aimingLineComponent;
    Vector3 lineOriginVertex = new Vector3();
    

    // TODO: Streamline initialization of this List and serialize it in a separate file.
    List<Material> aimingMats = new List<Material>(1);

    /* 
     * 
     * Physics and control fields
     * 
     */
    Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);   // Stores velocity data for a 2D plane (technically 3D but ignoring Z-axis).
    Rigidbody2D p_physics;                              // RigidBody component, this is used to call the physics engine.
    bool obeysGravity;                                  // Determines if player obeys gravity. To-be-used.
    float sprintFactor = 1.0f;                          // Controls how fast sprinting is for the player. 1 = player is not sprinting. 

    /*
     * Player Properties
     * Health, Regeneration, etc.
     */
    private float p_health;

    public float Health {
        get { return p_health; }
        private set { p_health = value; }
    }

    private float p_max_health;

    public float MaxHealth
    {
        get { return p_max_health; }
        private set { p_max_health = value; }
    }


    private float p_regen_factor;

    public float RegenModifier { 
        get { return p_regen_factor; }
        private set { p_regen_factor = value; }
    }

    private float p_mana;

    public float Mana
    {
        get { return p_mana; }
        private set { p_mana = value; }
    }

    private float p_max_mana;

    public float MaxMana
    {
        get { return p_max_mana; }
        private set { p_max_mana = value; }
    }


    // Movement data
    private enum direction : byte
    {
        North,
        East,
        South,
        West
    }

    private enum motion_state : byte
    {
        Dashing,
        Walking,
        Slow_Walking,
        Idle
    }

    private direction face_dir;
    private motion_state motion;
    // Use this for initialization
    void Start()
    {
        instance = GlobalVars.Instance;

        keybind = keybindobject.GetComponent<KeyBindScript>();

        p_anim_controller = GetComponent<Animator>();

        // [TENTATIVE]: Player will not be under effect of gravity except maybe for special circumstances.
        p_physics = GetComponent<Rigidbody2D>();
        p_physics.gravityScale = 0.0f;
        obeysGravity = false;

        aimingLineComponent = aimingLineContainer.GetComponent<LineRenderer>();
        aimingLineComponent.enabled = true;
        aimingLineComponent.GetMaterials(aimingMats);

        // [TENTATIVE]: Idle or starting face direction is South.
        face_dir = direction.South;
        motion = motion_state.Idle;

        // Stat intialization.
        p_max_health = instance.def_max_health;
        p_health = p_max_health;
        p_regen_factor = instance.def_regen_factor;

        p_max_mana = instance.def_max_energy;
        p_mana = p_max_mana;

    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKey(keybind.keys["Dash"]) && p_mana > 5)
        {
            motion = motion_state.Dashing;
            sprintFactor = 20.0f;

            p_mana -= 3f * Time.deltaTime;

        }
        else
        {
            p_mana += 2 * Time.deltaTime;
        }

        // Look for WASD input and act on that context.
        if (Input.GetKey(keybind.keys["Up"]) && !Input.GetKeyDown(keybind.keys["Down"]))
        { 
            if(motion == motion_state.Idle ) { p_anim_controller.Play("north_walk", 0); }
                 //|| Input.GetKey(keybind.keys["Down"])                    // Play animation to make sprite face north.
            face_dir = direction.North;
            motion = motion_state.Walking;
            movement = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;                // Actually move the sprite.
        }
        if (Input.GetKeyUp(keybind.keys["Up"]))
        {
            motion = motion_state.Idle;
        }
        // Same process for the other three Key reading If-statements.
        if (Input.GetKey(keybind.keys["Left"]) && !Input.GetKeyDown(keybind.keys["Right"]))
        {
            if (motion == motion_state.Idle ) { p_anim_controller.Play("west_walk", 0); }
             // || Input.GetKey(keybind.keys["Right"])
            face_dir = direction.West;
            motion = motion_state.Walking;
            movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;
        }
        if (Input.GetKeyUp(keybind.keys["Left"]))
        {
            motion = motion_state.Idle;
        }

        if (Input.GetKey(keybind.keys["Down"]) && !Input.GetKeyDown(keybind.keys["Up"]))
        {
            if (motion == motion_state.Idle ) { p_anim_controller.Play("south_walk", 0); }
               // || Input.GetKey(keybind.keys["Up"])
            face_dir = direction.South;
            motion = motion_state.Walking;
            movement = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;
        }
        if (Input.GetKeyUp(keybind.keys["Down"]))
        {
            motion = motion_state.Idle;
        }

        if (Input.GetKey(keybind.keys["Right"]) && !Input.GetKeyDown(keybind.keys["Left"]))
        {
            if (motion == motion_state.Idle ) { p_anim_controller.Play("east_walk", 0); }
                //|| Input.GetKey(keybind.keys["Left"])
            face_dir = direction.East;
            motion = motion_state.Walking;
            movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;
        }
        if (Input.GetKeyUp(keybind.keys["Right"]))
        {
            motion = motion_state.Idle;
        }
        // Determine and display idle state if there is no input.
        if (!Input.GetKey(keybind.keys["Up"]) && !Input.GetKey(keybind.keys["Left"]) && !Input.GetKey(keybind.keys["Down"]) && !Input.GetKey(keybind.keys["Right"]))
        {
            motion = motion_state.Idle;
            switch (face_dir)
            {
                case direction.North:
                    p_anim_controller.Play("north_idle", 0, 0f);
                    break;
                case direction.South:
                    p_anim_controller.Play("south_idle", 0, 0f);
                    break;
                case direction.East:
                    p_anim_controller.Play("east_idle", 0, 0f);
                    break;
                case direction.West:
                    p_anim_controller.Play("west_idle",0, 0f);
                    break;
                default: break;

            }

        }

        if(aimingLineComponent.enabled)
        {
            lineOriginVertex = playerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            lineOriginVertex.z = 0; // Necessary since lines are rendered in 3D. Without this assignment of Z, the line will point "up" or "down" towards where Unity would think the mouse is in 3D, causing Tiling issues on the materials.
            aimingLineComponent.SetPosition(0, this.transform.position);
            aimingLineComponent.SetPosition(1, lineOriginVertex);
            aimingMats[0].mainTextureScale = new Vector2(1, 1);
        }

        // Reset sprint factor to 1.0 if it's above 1.0
        sprintFactor = sprintFactor > 1.0f ? 1.0f : 1.0f;

        // ATTACK INPUTS
        if (Input.GetKey(keybind.keys["Primary_Fire"]))
        {
            if (p_mana > 52.9f && castCooldown <= 0f)
            {
                Vector3 aimVector = playerCam.ScreenToWorldPoint(Input.mousePosition);
                GameObject attackProjectile;
                attackProjectile = Instantiate(spell, transform.position, transform.rotation);
                Rigidbody2D projPhysics = attackProjectile.GetComponent<Rigidbody2D>();
                FireballScript controller = attackProjectile.GetComponent<FireballScript>();
                controller.owner = "Player";
                aimVector.z = 0;

                Vector2 direction = aimVector - transform.position;
                direction.Normalize();
                projPhysics.velocity = direction * 20;

                p_mana -= 20;
                castCooldown = 3.0f;
            }
        }

        if(castCooldown > 0f)
        {
            castCooldown -= Time.deltaTime;
        }

        // DEBUG KEYS

        if (p_health <= 0)
        {
            SceneTransition.LoadSceneSwitch("Scenes/MainMenu");
        }

        if(Input.GetKey(KeyCode.RightShift))
        {
            p_health -= Time.deltaTime * 5;
        }
    }

    public void TakeDamage(float damage)
    {
        p_health -= damage;
        Debug.Log("Player took " + damage + " damage, health now: " + p_health);
    }
}
