using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindScript : MonoBehaviour
{

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); //<-- make private?

    // Start is called before the first frame update

    //create (default?) keybinds (subject to change)

    // TODO: load Start controls from a file. (JSON)
    void Start()
    {
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Dash", KeyCode.LeftShift);
        keys.Add("Slow_Walk", KeyCode.LeftAlt);
        keys.Add("Primary_Fire", KeyCode.Mouse0);
        keys.Add("Alt_Fire", KeyCode.Mouse1);
        keys.Add("Absorb", KeyCode.Space);
        keys.Add("Interact", KeyCode.F);

    }
    
    public void modControl(KeyCode something, string control)
    {

    }

}
