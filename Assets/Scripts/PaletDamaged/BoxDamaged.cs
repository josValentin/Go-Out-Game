using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamaged : MonoBehaviour
{
    [SerializeField] private string targetTag = "Tool Axe";
    [SerializeField] private string soundId = "box_damaged";
    [SerializeField] private Texture2D damaged_1;
    [SerializeField] private Texture2D damaged_2;
    [SerializeField] private ItemData itemData;

    int damageAllowed = 3;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            damageAllowed--;
            Material mat = new Material(GetComponent<MeshRenderer>().sharedMaterial);

            if (damageAllowed == 2)
            {
                mat.mainTexture = damaged_1;
                GetComponent<MeshRenderer>().sharedMaterial = mat;

            }
            else if (damageAllowed == 1)
            {
                mat.mainTexture = damaged_2;
                GetComponent<MeshRenderer>().sharedMaterial = mat;
            }
            else
            {
                Destroy(gameObject);
                if (itemData != null)
                {
                    Inventory.Add(itemData);
                    ShortPopupUI.Show($"{itemData.itemName} obtained");
                }
            }
            SoundManager.PlayOneShot(soundId, 0.5f);
        }
        else
        {
            if (other.tag.StartsWith("Tool "))
            {
                string toolName = targetTag.Replace("Tool ", "");
                string text = (toolName.StartsWith("A") || toolName.StartsWith("a")) ? "an " + toolName : "a " + toolName;
                ShortPopupUI.Show($"You need {text} to destroy this object");
            }
        }
    }
}
