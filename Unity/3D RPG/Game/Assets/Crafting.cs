using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<string> Items;
    public List<int> ItemCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddItem(string ItemName, int ItemsAdded)
    {
        int ItemIndex = FindIndexByName(ItemName);
        ItemCount[ItemIndex] += ItemsAdded;
    }

    public int FindIndexByName(string ItemName)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemName == Items[i])
            {
                Debug.Log("Item " + ItemName + " was added at slot " + i);
                return i;
            }
        }

        return 0;
    }
}