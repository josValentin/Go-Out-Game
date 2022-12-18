using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnCollet;
    public ItemData itemData;

    public ItemData CollectItem()
    {
        Destroy(gameObject);
        OnCollet?.Invoke();
        return itemData;
    }
}
