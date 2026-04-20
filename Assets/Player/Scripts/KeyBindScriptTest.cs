using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScriptTest : MonoBehaviour
{

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); //<-- make private?

    public Text up, down, left, right, dash, slow_walk, primary_fire, alt_fire, absorb, interact;

    private GameObject currentKey;

    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    // Start is called before the first frame update

    //create (default?) keybinds (subject to change)

    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up","W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Dash", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dash", "LeftShift")));
        keys.Add("Slow_Walk", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Slow_Walk", "LeftAlt")));
        keys.Add("Primary_Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Primary_Fire", "Mouse0")));
        keys.Add("Alt_Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Alt_Fire", "Mouse1")));
        keys.Add("Absorb", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Absorb", "Space")));
        keys.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F")));

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        dash.text = keys["Dash"].ToString();
        slow_walk.text = keys["Slow_Walk"].ToString();
        primary_fire.text = keys["Primary_Fire"].ToString();
        alt_fire.text = keys["Alt_Fire"].ToString();
        absorb.text = keys["Absorb"].ToString();
        interact.text = keys["Interact"].ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }
        
        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }
        
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }

        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }

        if (Input.GetKeyDown(keys["Dash"]))
        {
            Debug.Log("Dash");
        }

        if (Input.GetKeyDown(keys["Slow_Walk"]))
        {
            Debug.Log("Slow_Walk");
        }

        if (Input.GetKeyDown(keys["Primary_Fire"]))
        {
            Debug.Log("Primary_Fire");
        }

        if (Input.GetKeyDown(keys["Alt_Fire"]))
        {
            Debug.Log("Alt_Fire");
        }

        if (Input.GetKeyDown(keys["Absorb"]))
        {
            Debug.Log("Absorb");
        }

        if (Input.GetKeyDown(keys["Interact"]))
        {
            Debug.Log("Interact");
        }
    }


    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
            if (e.isMouse)
            {
                switch (e.button)
                {
                    case 0:
                        keys[currentKey.name] = KeyCode.Mouse0;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse0";
                        break;
                    case 1:
                        keys[currentKey.name] = KeyCode.Mouse1;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse1";
                        break;

                }

                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }

        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    public void ResetKeysToDefault()
    {
        keys["Up"] = KeyCode.W;
        up.GetComponent<Text>().text = keys["Up"].ToString();
        keys["Down"] = KeyCode.S;
        down.GetComponent<Text>().text = keys["Down"].ToString();
        keys["Left"] = KeyCode.A;
        left.GetComponent<Text>().text = keys["Left"].ToString();
        keys["Right"] = KeyCode.D;
        right.GetComponent<Text>().text = keys["Right"].ToString();
        keys["Dash"] = KeyCode.LeftShift;
        dash.GetComponent<Text>().text = keys["Dash"].ToString();
        keys["Slow_Walk"] = KeyCode.LeftAlt;
        slow_walk.GetComponent<Text>().text = keys["Slow_Walk"].ToString();
        keys["Primary_Fire"] = KeyCode.Mouse0;
        primary_fire.GetComponent<Text>().text = keys["Primary_Fire"].ToString();
        keys["Alt_Fire"] = KeyCode.Mouse1;
        alt_fire.GetComponent<Text>().text = keys["Alt_Fire"].ToString();
        keys["Absorb"] = KeyCode.Space;
        absorb.GetComponent<Text>().text = keys["Absorb"].ToString();
        keys["Interact"] = KeyCode.F;
        interact.GetComponent<Text>().text = keys["Interact"].ToString();
    }

}
