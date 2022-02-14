using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnInventoryChange += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].RemoveItem();
            }
        }
    }
}
