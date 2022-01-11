using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Animator p_anim_controller;
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
        face_dir = direction.South;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Playing North assets");
            p_anim_controller.Play("north_walk", 0);
            face_dir = direction.North;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Playing West assets");
            p_anim_controller.Play("west_walk", 0);
            face_dir = direction.West;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Playing South assets");
            p_anim_controller.Play("south_walk", 0);
            face_dir = direction.South;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Playing East assets");
            p_anim_controller.Play("east_walk", 0);
            face_dir = direction.East;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if (face_dir == direction.North) { p_anim_controller.Play("north_idle", 0, 0f); }
            if (face_dir == direction.West) { p_anim_controller.Play("west_idle", 0, 0f); }
            if (face_dir == direction.South) { p_anim_controller.Play("south_idle", 0, 0f); }
            if (face_dir == direction.East) { p_anim_controller.Play("east_idle", 0, 0f); }
        }

    }
}
