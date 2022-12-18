using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    #region Private vals
    #endregion
    #region SerializeFielded vals
    #endregion
    #region Public vals
    public ItemData data { get; private set; }
    public int stackSize { get; set; }
    #endregion


    public InventoryItem(ItemData item)
    {
        data = item;
        AddToStack();
    }

    public void AddToStack(int amount = 1)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount = 1)
    {
        stackSize -= amount;
    }
}
