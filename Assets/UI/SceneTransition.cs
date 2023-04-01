using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private string _nextScenePath = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Portal collided with object tagged as: " + collision.tag);

        Debug.Log("Attempting to load next Scene...");
        SceneManager.LoadScene(_nextScenePath, LoadSceneMode.Single);
    }
}
