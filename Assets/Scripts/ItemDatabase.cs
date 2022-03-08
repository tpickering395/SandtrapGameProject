using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour
{
    private string itemsPath;
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemsPath = Application.dataPath + "/StreamingAssets/Items.json";

        // This might be useful later on if the DatabaseInit() fails.
        // itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        if (File.Exists(itemsPath) && DatabaseInit())
        {
            Debug.Log($"Database Initialized with {database.Count} items...");
            Debug.Log(database[1].value);
            Debug.Log("\n");
            Debug.Log(database[1].stats.decay);
            Debug.Log("\n");
            Debug.Log(database[1].stats.Count);
        }
        else
        {
            Debug.Log($"Database Initialization either failed or could not find item JSON file: \nJSON Detected: {File.Exists(itemsPath)}\nDatabase Count: {database.Count}");
        }

    }

    bool DatabaseInit()
    {
        database = JsonMapper.ToObject<List<Item>>(File.ReadAllText(itemsPath));
        return database.Count > 0 ? true : false;
    }


}

/*  Example Item in JSON:
 *   "id": 1,
 *   "displayName": "Soy Sauce",
 *   "value": 9,
 *   "stats": {
 *     "strength": 1,
 *     "nutrition": 2,
 *     "decay": 80
 *   },
 *   "description": "A bottle of soy sauce, perfect for crafting tasty meals.",
 *   "stackable": true,
 *   "rarity": 1,
 *   "slug": "soy_bottle"
 * 
 */

public class Stats
{
    public int strength { get; set; }
    public int nutrition { get; set; }
    public int decay { get; set; }
}
public class Item
{
    public int id { get; private set; }
    public string displayName { get; private set; }
    public int value { get; set; }
    public Stats stats;
    public string description { get; set; }
    public bool stackable { get; set; }
    public int rarity { get; set; }
    public string slug { get; set; }


    public Item(int id, string name, int value)
    {
        this.id = id;
        this.displayName = name;
        this.value = value;
    }

    public Item()
    {
        this.id = int.MinValue;
    }
}