using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAcquisition : MonoBehaviour
{
    public Item item;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log(item.name + "item picked up");
            if(Inventory.instance.Add(item))
                Destroy(gameObject);
        }
    }
}
