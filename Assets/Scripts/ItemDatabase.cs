using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour
{
    private string itemsPath;
    private List<Item> database = new List<Item>();
    private Dictionary<int, Item> lookup_table = new Dictionary<int, Item>();   // Used for O(1) lookup time when checking if an item exists in the database.
    private JsonData itemData;

    private GlobalVars instance = GlobalVars.Instance;

    void Start()
    {
        itemsPath = Application.dataPath + "/StreamingAssets/Items.json";

        // This might be useful later on if the DatabaseInit() fails.
        // itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        if (File.Exists(itemsPath) && DatabaseInit())
        {
            if (instance.debug)
            {
                Debug.Log($"Database Initialized with {database.Count} items...");
                Debug.Log(database[1].value);
                Debug.Log("\n");
                Debug.Log(database[1].stats.decay);

                Debug.Log($"\n \nOutput of Lookup Table initialization: Size: {lookup_table.Count} \nKeys: {lookup_table.Keys}\n Values: {lookup_table.Values}\n");
                // Debug.Log("\n");
                // Debug.Log(database[1].stats.Count);
            }
        }
        else
        {
            if (instance.debug)
            {
                Debug.Log($"Database Initialization either failed or could not find item JSON file: \nJSON Detected: {File.Exists(itemsPath)}\nDatabase Count: {database.Count}");
                Debug.Log($"\n \nOutput of Lookup Table initialization: Size: {lookup_table.Count} \nKeys: {lookup_table.Keys}\n Values: {lookup_table.Values}\n");
            }
        }

    }


    bool DatabaseInit()
    {
        return DatabaseMap() && InitializeLookupTable();

    }

    bool DatabaseMap()
    {
        database = JsonMapper.ToObject<List<Item>>(File.ReadAllText(itemsPath));
        return database.Count > 0 ? true : false;
    }
    
    bool InitializeLookupTable()
    {
        for(int i = 0; i < database.Count; i++)
        {
            lookup_table.Add(database[i].id, database[i]);      // Attach the ID as a key to the item data
            database[i].initializeSpritePath();
        }

        return lookup_table.Count > 0 ? true : false;
    }
    public Item getItemByID(int id)
    {
        Item temp;
        if(lookup_table.TryGetValue(id, out temp))          // Attempt lookup of the item.
        {
            return temp;
        }
        return null;
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
 *   "slug": "soy"
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
    public Sprite sprite { get; set; }

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

    public bool initializeSpritePath()
    {
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
        return sprite != null ? true : false;
    }
}