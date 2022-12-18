using UnityEngine;

public class PickupItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableItem"))
        {
            ItemData itemData = other.gameObject.GetComponent<CollectableItem>().CollectItem();
            Inventory.Add(itemData);
        }
    }
}
