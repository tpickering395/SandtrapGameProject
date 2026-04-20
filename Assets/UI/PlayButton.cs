using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
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

    // Explicitly declare public because we don't want someone to think they can make this private. A private OnClick function will cause Unity to be unable to call this function
    //     from its runtime binder.
    public void OnClick()
    {
        SceneManager.LoadScene(_nextScenePath, LoadSceneMode.Single);
    }
}
