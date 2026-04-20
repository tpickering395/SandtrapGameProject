using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    private GlobalVars instance;

    GameObject player;

    PlayerScript playerStats;

    public float max_health;

    public float max_mana;

    [SerializeField]
    public float current_mana;

    [SerializeField]
    public float current_health;

    private Image healthBar;
    private Image manaBar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        instance = GlobalVars.Instance;

        playerStats = player.GetComponent<PlayerScript>();

        // Get player stats.
        max_health = playerStats.MaxHealth;
        current_health = playerStats.Health;
        max_mana = playerStats.MaxMana;
        current_mana = playerStats.Mana;

        // Get UI markers.
        healthBar = GameObject.Find("Health").GetComponent<Image>();
        manaBar = GameObject.Find("ManaBar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        // Update stats
        max_health = playerStats.MaxHealth;
        current_health = playerStats.Health;
        max_mana = playerStats.MaxMana;
        current_mana = playerStats.Mana;

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

       /* if(Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log("Color values = " + r + " " + g + " " + b + " ");
            Debug.Log("Health values = " + current_health + " " + max_health + " " + healthPercent);

            Debug.Log("Mana UI: Manabar status: " + (manaBar != null));
            Debug.Log("Player Mana status: " + current_mana + " " + max_mana);

            Debug.Log("ManaBar status: " + manaBar.fillAmount);
        }*/

        manaBar.fillAmount = current_mana / max_mana;

    }
}
