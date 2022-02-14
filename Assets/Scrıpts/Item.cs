using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public string itemName = "new item";
	public Sprite icon = null;
	public int defenseModifier;
	public int attackModifier;
}
