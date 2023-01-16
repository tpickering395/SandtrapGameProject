using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public bool stackable = true;

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        AddToStack();
    }
    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }

    public override string ToString()
    {
        return data.id + " : " + data.displayTag;
    }
}
