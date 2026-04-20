using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField]
    public GameObject projectile;
    private float npc_health;
    private bool fireReady = false;

    public float Health
    {
        get { return npc_health; }
        private set { npc_health = value; }
    }

    private float npc_max_health;

    public float MaxHealth
    {
        get { return npc_max_health; }
        private set { npc_max_health = value; }
    }


    private float npc_regen_factor;

    public float RegenModifier
    {
        get { return npc_regen_factor; }
        private set { npc_regen_factor = value; }
    }

    private float npc_mana;

    public float Mana
    {
        get { return npc_mana; }
        private set { npc_mana = value; }
    }

    private float npc_max_mana;

    public float MaxMana
    {
        get { return npc_max_mana; }
        private set { npc_max_mana = value; }
    }

    [SerializeField]
    private RadialObjectDetection detector;

    private float moveAdjustDelay = 0.15f;

    private float shootDelay = 5.0f;

    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        npc_max_health = 50;
        npc_health = npc_max_health;
    }

    // Update is called once per frame
    void Update()
    {
 /*       
       if(detector != null && detector.ObjectDetected)
        {
            Debug.Log("No null on detector and objectDetected");
        }
        if(detector != null && Vector3.Distance(transform.position, detector.TriggerTarget.transform.position) > 3.5f)
        {
            Debug.Log("Distance check cleared");
        }
 */
        if (detector != null && detector.ObjectDetected && (Vector3.Distance(transform.position, detector.TriggerTarget.transform.position) > 3.5f) && moveAdjustDelay >= 0f)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, detector.TriggerTarget.transform.position, step);

            moveAdjustDelay -= Time.deltaTime;
        }
        if(moveAdjustDelay < 0f)
        {
            moveAdjustDelay = 0.15f;
        }
        if(shootDelay <= 0f)
        {
            fireReady = true;
        }
        if(shootDelay > 0f)
        {
            shootDelay -= Time.deltaTime;
        }
        else
        {
            if (detector != null && detector.ObjectDetected && fireReady)
            {

                shootDelay = 5.0f;
                GameObject attackProjectile;
                attackProjectile = Instantiate(projectile, transform.position, transform.rotation);
                Rigidbody2D projPhysics = attackProjectile.GetComponent<Rigidbody2D>();

                FireballScript controller = attackProjectile.GetComponent<FireballScript>();
                controller.owner = "Enemy";

                Vector2 direction = detector.TargetAimVector;
                direction.Normalize();

                projPhysics.velocity = direction * 20;

                shootDelay = 3.0f;
            }
        }
        if(npc_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        npc_health -= damage;
    }
}
