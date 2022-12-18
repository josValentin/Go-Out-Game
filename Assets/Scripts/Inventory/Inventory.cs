using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public static Dictionary<ItemData, InventoryItem> items = new Dictionary<ItemData, InventoryItem>();

    public static void Add(ItemData itemData)
    {
        if (items.TryGetValue(itemData, out InventoryItem item))
        {
            if (!itemData.isStackable) return;

            item.AddToStack();
            GamplayInvetory.Instance.UpdateSlotStack(item);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            items.Add(itemData, newItem);
            GamplayInvetory.Instance.AddToIventory(newItem);
        }
    }

    public static InventoryItem Add_Backend(ItemData itemData, int stackSize = 1)
    {
        if (items.TryGetValue(itemData, out InventoryItem item))
        {
            if (!itemData.isStackable) return item;

            item.AddToStack(stackSize);
            return item;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            newItem.stackSize = stackSize;
            items.Add(itemData, newItem);           
            return newItem;
        }
    }

    public static void Remove(ItemData itemData, int amount = 1)
    {
        if (amount <= 0) return;

        if (items.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack(amount);
            if (item.stackSize <= 0)
            {
                items.Remove(itemData);
                GamplayInvetory.Instance.Remove(item);
            }
            else
                GamplayInvetory.Instance.UpdateSlotStack(item);

        }
    }

    public static InventoryItem Get(ItemData itemData)
    {
        if (items.TryGetValue(itemData, out InventoryItem item)) return item;

        return null;
    }
}
