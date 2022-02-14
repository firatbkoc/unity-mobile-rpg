using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
	public event Action OnInventoryChange; 
	public List<Item> items = new List<Item>();
	public int numberOfSlots = 6;
	public static Inventory instance;

	public int attackMods;
	public int defenseMods;

	void Awake()
	{
		if (instance != null)
		{
			return;
		}
		instance = this;
	}

	public bool Add(Item item)
	{
		if (items.Count >= numberOfSlots)
			return false;

		items.Add(item);
        
		//add mods
		attackMods += item.attackModifier;
		defenseMods += item.defenseModifier;

		OnInventoryChange?.Invoke();

		return true;
    }

	public void Remove(Item item)
    {
		items.Remove(item);
        
		//remove mods
		attackMods -= item.attackModifier;
		defenseMods -= item.defenseModifier;

		OnInventoryChange?.Invoke();
	}
}
