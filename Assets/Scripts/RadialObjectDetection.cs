using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadialObjectDetection : MonoBehaviour
{
    private Transform triggerDetector;
    private GameObject triggerTarget;

    public bool ObjectDetected { get; private set; }

    public GameObject TriggerTarget
    {
        get { return triggerTarget; }
        private set
        {
            triggerTarget = value;
            ObjectDetected = triggerTarget != null;
            Debug.Log("Detection status changed: " + ObjectDetected);
            Debug.Log("Trigger target null? " + triggerTarget == null);
        }
    }


    public Vector2 TargetAimVector => triggerTarget.transform.position - rCastOrigin.position;

    [Header("RadialCast")]
    [SerializeField]
    private Transform rCastOrigin;
    public float castRadius = 1f;
    public float offset = 0f;

    public float radialCastInterval = 0.25f;

    public int radialCastDelay = 3;

    public LayerMask radialCastFilter;

    public string targetTag = "";
    [Header("Debug Parameters")]
    public Color wfpassiveColor = Color.green;
    public Color wfaggroColor = Color.red;
    public bool debug_mode = true;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TriggerCheckCoroutine());
    }


    IEnumerator TriggerCheckCoroutine()
    {
        yield return new WaitForSeconds(radialCastDelay);
        RadialCast();
        StartCoroutine(TriggerCheckCoroutine());
    }


    public void RadialCast()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)rCastOrigin.position, castRadius, radialCastFilter);
        if(collider != null && collider.CompareTag(targetTag))
        {
            TriggerTarget = collider.gameObject;
/*            Debug.Log("Detected " + collider.gameObject);
            Debug.Log("Detection status " + ObjectDetected);
            Debug.Log("TargetTrigger status " + triggerTarget == null);*/
        }
        else
        {
            TriggerTarget = null;
            Debug.Log("Detected nothing");
        }
    }

    // Debug Overlay
    void OnDrawGizmos()
    {
        if(debug_mode && rCastOrigin != null)
        {
            Gizmos.color = wfpassiveColor;
            if(ObjectDetected)
            {
                Gizmos.color = wfaggroColor;
                Gizmos.DrawLine(new Vector3(rCastOrigin.position.x, rCastOrigin.position.y, 0), TriggerTarget.transform.position);
            }
            Gizmos.DrawWireSphere(new Vector3(rCastOrigin.position.x, rCastOrigin.position.y, 0), castRadius);
        }
    }
}
