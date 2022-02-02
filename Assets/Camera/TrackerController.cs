using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    public Transform playerPosition;
    public float updateRate = 3;
    public Vector2 trackingOffset;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = (Vector3)trackingOffset;
        offset.z = transform.position.z - playerPosition.position.z;
    }

    // LateUpdate ensures camera moves in a reactive sense and not a proactive way.
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.position + offset, updateRate * Time.deltaTime);
    }
}
