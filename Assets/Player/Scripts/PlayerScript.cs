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


    /* 
     * 
     * Physics and control fields
     * 
     */
    Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);   // Stores velocity data for a 2D plane (technically 3D but ignoring Z-axis).
    Rigidbody2D p_physics;                              // RigidBody component, this is used to call the physics engine.
    bool obeysGravity;                                  // Determines if player obeys gravity.
    float sprintFactor = 1.0f;                          // Controls how fast sprinting is for the player. 1 = player is not sprinting. 
        
    private enum direction : byte
    {
        North,
        East,
        South,
        West
    }
    private direction face_dir;
    // Use this for initialization
    void Start()
    {
        p_anim_controller = GetComponent<Animator>();
        
        // [TENTATIVE]: Player will not be under effect of gravity except maybe for special circumstances.
        p_physics = GetComponent<Rigidbody2D>();
        p_physics.gravityScale = 0.0f;
        obeysGravity = false;

        // [TENTATIVE]: Idle or starting face direction is South.
        face_dir = direction.South;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            sprintFactor = 2.0f;
        }

        // Look for WASD input and act on that context.
        if (Input.GetKey(KeyCode.W))
        {
            
            Debug.Log("Playing North assets");
            p_anim_controller.Play("north_walk", 0);                        // Play animation to make sprite face north.
            face_dir = direction.North;
            movement = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;                // Actually move the sprite.
        }
        // Same process for the other three Key reading If-statements.
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Playing West assets");
            p_anim_controller.Play("west_walk", 0);
            face_dir = direction.West;
            movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Playing South assets");
            p_anim_controller.Play("south_walk", 0);
            face_dir = direction.South;
            movement = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Playing East assets");
            p_anim_controller.Play("east_walk", 0);
            face_dir = direction.East;
            movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            transform.position += movement * Time.deltaTime * sprintFactor;
        }
        // Determine and display idle state if there is no input.
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if (face_dir == direction.North) { p_anim_controller.Play("north_idle", 0, 0f); }
            if (face_dir == direction.West) { p_anim_controller.Play("west_idle", 0, 0f); }
            if (face_dir == direction.South) { p_anim_controller.Play("south_idle", 0, 0f); }
            if (face_dir == direction.East) { p_anim_controller.Play("east_idle", 0, 0f); }
        }

        // Reset sprint factor to 1.0 if it's above 1.0
        sprintFactor = sprintFactor > 1.0f ? 1.0f : 1.0f;

    }
}
