using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    private GlobalVars instance;
   
    public float max_health;

    [SerializeField]
    public float current_health;

    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        instance = GlobalVars.Instance;
        max_health = player.GetComponent<PlayerScript>().MaxHealth;
        current_health = player.GetComponent<PlayerScript>().Health;

        healthBar = GetComponent<Image>();

        Debug.Log("Health UI: healthbar status: " + (healthBar != null));
        Debug.Log("Player Health status: " + current_health + " " + max_health);

    }

    // Update is called once per frame
    void Update()
    {
        // Update Health bar fill.
        float healthPercent = current_health / max_health;
        healthBar.fillAmount = healthPercent;

        float r, g, b;
        r = 0;
        g = 1;
        b = 0;
        if(healthPercent > 0.5f)
        {
            r = (1 - healthPercent) * 2;
        }
        if(healthPercent == 0.5f)
        {
            r = 1;
            g = 1;
        }
        if(healthPercent < 0.5f)
        {
            r = 1;
            g = healthPercent * 2;
        }

        healthBar.color = new Color(r, g, b);

        if(Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log("Color values = " + r + " " + g + " " + b + " ");
            Debug.Log("Health values = " + current_health + " " + max_health + " " + healthPercent);
        }
    }
}
