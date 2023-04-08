using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField]
    public float damage;

    public float timeout;

    public string owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0f && gameObject != null)
        {
            Debug.Log("fireball destroys itself to decay");
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with " + collider);

        if (collider.CompareTag("Player") && owner != "Player")
        {
            Debug.Log("Fire ball hit Player");
            PlayerScript ply = GameObject.Find("Player").GetComponent<PlayerScript>();
            ply.TakeDamage(damage);
            Debug.Log("fireball destroys itself to hitting player");
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Enemy") && owner != "Enemy")
        {
            FireSpirit fs = collider.gameObject.GetComponent<FireSpirit>();
            fs.TakeDamage(damage);
            Debug.Log("fireball destroys itself to hitting enemy");
            Destroy(gameObject);
        }


    }
}
