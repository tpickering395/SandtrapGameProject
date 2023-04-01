using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Exit the game when "Quit" is clicked.
    public void OnClick()
    {
        Debug.Log("Quit button was clicked.");
        Application.Quit();
    }
}
