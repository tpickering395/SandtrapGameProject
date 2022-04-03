using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject inventoryUI;
    GameObject slotUI;

    ItemDatabase db;

    public GameObject slot_prefab;
    public GameObject inventoryItem;

    int maxSlots;
    int debug = 0;

    public List<Item> itemList = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    public Dictionary<int, string> lookuptable = new Dictionary<int, string>();
    void Start()
    {
        db = GetComponent<ItemDatabase>();

        maxSlots = 20;
        inventoryUI = GameObject.Find("Inventory Panel");
        slotUI = inventoryUI.transform.Find("Slot Panel").gameObject;

        for (int i = 0; i < maxSlots; i++)
        {
            itemList.Add(new Item());
            slots.Add(Instantiate(slot_prefab));
            slots[i].transform.SetParent(slotUI.transform);
            slots[i].transform.localScale = new Vector3(1, 1, 1);
        }
        Debug.Log("Attempting add function...");
        Debug.Log($"Debug = {debug}");
        AddItem(1);
        Debug.Log($"Debug = {debug}");
        AddItem(1);
        Debug.Log($"Debug = {debug}");
        AddItem(1);
        Debug.Log($"Debug = {debug}");
        Debug.Log("Add called.");
        Debug.Log("Attempting secondary addition...");
        AddItem(0);
        Debug.Log("Check Visually!");
    }

    public void AddItem(int id)
    {
        Item candidate = db.getItemByID(id);
        if (candidate.stackable && existsInContainer(id)) 
        {
            
            string temp;
            lookuptable.TryGetValue(id, out temp);
            int inventory_index;
            bool success = int.TryParse(temp, out inventory_index);

            if(!success)
            {
                Debug.LogError($"ERROR: Integer parse on lookup table Value failed.\nData:\nKey: {id}\nValue:{temp}\nParse Result:{inventory_index}");
                return;
            }

            ItemData data = slots[inventory_index].transform.GetChild(0).GetComponent<ItemData>();
            data.count++;
            data.transform.GetChild(0).GetComponent<Text>().text = data.count.ToString();
            return;
        }

        for(int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i].id == int.MinValue)
            {
                itemList[i] = candidate;

                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.transform.SetParent(slots[i].transform, false);
                itemObj.GetComponent<Image>().sprite = candidate.sprite;
                itemObj.transform.position = Vector2.zero;
               // itemObj.transform.localScale = new Vector3(10, 10, 1);  Hardwiring the scaling, might serialize this as a public field for unity editor/artists.
                itemObj.name = candidate.displayName;

                Debug.Log($"Item added to Slot UI Panel #: {i}, id: {itemList[i].id}");

                lookuptable.Add(id, i.ToString());
                break;
            }
        }
    }

    public bool existsInContainer(int id)
    {
        string result;
        lookuptable.TryGetValue(id, out result);
        Debug.Log($"Result from container check is: {result}");
        return result == null ? false : true;
    }
}
