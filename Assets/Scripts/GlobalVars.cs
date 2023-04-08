using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Global Variable container class.
 * Serialize Fields that should be modifiable from the inspector.
 * Debug should remain as true unless a public build is being compiled for release.
 * Theoretically, this class should be thread-safe, but be aware of the possible implications of multi-threaded processes accessing this construct.
 */
public sealed class GlobalVars : MonoBehaviour { 

    [SerializeField] public bool debug = false;
    [SerializeField] public bool god = false;
    [SerializeField] public int  frameRate = 60;
    [SerializeField] public bool followChar = true;
    [SerializeField] public bool vSync = false;
    [SerializeField] public bool fullscreen = false;


    /* Game Default variables are declared here.
     * This will be mainly used on first load (i.e. games without save files)
     * 
     * These should not be serialized to the editor.
     * Keep this intact.
     */
    public float def_max_health = 100f;
    public float def_regen_factor = 0.6f;
    public float def_max_energy = 90f;

    private GlobalVars() {}
    private static readonly object Locker = new object();
    private static GlobalVars instance = null;
    public static GlobalVars Instance
    {
        get
        {
            lock(Locker)
            {
                if(instance == null)
                {
                    instance = new GlobalVars();
                }
                return instance;
            }
        }
    }

    void OnGraphicsChange()
    {
        // TODO: Update values for graphics parameters.
    }

    void OnResolutionChange()
    {
        // TODO: Implement this.
    }

    void SaveSettings()
    {
        // TODO: Implement some json or key-value save file.
    }
}
