using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int itemId;
    public string itemName;
    public Sprite itemIcon;
    public bool shouldConvert;
    public abstract int Use<T>(T callback, params T[] args);
}
