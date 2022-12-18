using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SlotUI : MonoBehaviour
{
    private static Color32 selectedColor = Color.white;
    private static Color32 defaultColor = new Color32(140, 142, 139, 255);

    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text numTxt;
    [SerializeField] TMP_Text txtItemName;

    public InventoryItem Item { get; private set; }
    [HideInInspector] public bool isSlotEmpty = true;
    [HideInInspector] public bool isSelected;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GamplayInvetory.Instance.Select(this));
        txtItemName.alpha = 0;
        txtItemName.text = "";
    }

    public void ShowItemIcon(InventoryItem itemData)
    {
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = itemData.data.itemSprite;
        itemIcon.color = itemData.data.itemColor;
        isSlotEmpty = false;
        Item = itemData;
        if (isSelected) CheckTool();
    }

    public void Empty()
    {
        itemIcon.gameObject.SetActive(false);
        itemIcon.sprite = null;
        itemIcon.color = Color.white;
        isSlotEmpty = true;
        Item = null;
        numTxt.text = "";
        if (isSelected) CheckTool();
    }

    public void Select()
    {
        GetComponent<Image>().color = selectedColor;
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        if (Item != null)
        {
            txtItemName.text = Item.data.itemName;
            txtItemName.DOFade(1, 0.4f).OnComplete(() => txtItemName.DOFade(0, 0.4f));
        }

        isSelected = true;
        CheckTool();
    }

    public void Deselect()
    {
        GetComponent<Image>().color = defaultColor;

        transform.localScale = Vector3.one;
        isSelected = false;

    }

    public void UpdateStackText()
    {
        if (Item.stackSize == 1)
        {
            numTxt.text = "";
            return;
        }
        numTxt.text = Item.stackSize.ToString();
    }

    void CheckTool()
    {
        if (Item == null)
        {
            if (PlayerController.Instance != null)
                PlayerController.Instance.SwitchTool(null);
            return;
        }
        PlayerController.Instance.SwitchTool(Item.data);
    }
}
