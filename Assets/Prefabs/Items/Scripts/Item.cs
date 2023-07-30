using UnityEngine;

[CreateAssetMenu(filename = "New Item", menuName = "Item/Create")]

public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
}
