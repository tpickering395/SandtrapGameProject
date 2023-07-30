using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType;
{
    Equipment,
    Food,
    Default
}

public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public ItemType itemName;
    public int value;
    public string description;
}
