using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInHand : MonoBehaviour
{
    #region Private vals
    private Animator anim;
    #endregion
    #region Public vals
    //public ItemType type;
    public ItemData data;
    //public float power = 20;
    public UnityEngine.Events.UnityEvent OnUseItem;
    #endregion

    private void Start()
    {
        switch (data.type)
        {
            case ItemType.Tool:
                OnUseItem.AddListener(ToolAnimation);
                break;
            case ItemType.Consumable:
                OnUseItem.AddListener(() => Inventory.Remove(data));
                break;
            case ItemType.NonConsumable:
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        if (data.type != ItemType.Tool) return;
        if (!anim) anim = GetComponent<Animator>();
    }

    public void UseItem() => OnUseItem?.Invoke();

    private void ToolAnimation()
    {
        if (anim == null) return;
        anim.SetTrigger("ToolAnimation");
    }
}