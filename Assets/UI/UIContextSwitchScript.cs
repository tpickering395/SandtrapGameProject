using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIContextSwitchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvasToDisable;

    [SerializeField]
    private GameObject _canvasToEnable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log(_canvasToDisable.GetComponent<Canvas>());
        Debug.Log(_canvasToEnable.GetComponent<Canvas>());
        _canvasToDisable.GetComponent<Canvas>().enabled = false;
        _canvasToEnable.GetComponent<Canvas>().enabled = true;
    }
}
