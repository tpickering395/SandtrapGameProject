using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code coppied from this tutorial https://www.youtube.com/watch?v=2glgbPzLrc8 How to make draggable objects in Unity - 2018

public class UI_Drag : MonoBehaviour
{
    public GameObject Inventory_Panel;
    float OffsetX;
    float OffsetY;
    Vector3 bufferVector;
    Vector2 maxBound;
    Vector2 minBound;

    void Awake()
    {
        maxBound = GetComponent<RectTransform>().anchorMax; 
        minBound = GetComponent<RectTransform>().anchorMin;
    }

    public void BeginDrag()
    {
        OffsetX = transform.position.x - Input.mousePosition.x;
        OffsetY = transform.position.y - Input.mousePosition.y;
    }
    public void OnMouseDrag()
    {
        bufferVector = new Vector3(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
        print(bufferVector);
        if (bufferVector.x < 1468 && bufferVector.y < 730 && bufferVector.x > 0 && bufferVector.y > 0)
        {
            transform.position = new Vector3(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
        }
        
    }

}
