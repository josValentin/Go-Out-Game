using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;
    [SerializeField] private TextMeshProUGUI displayText;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public static void ShowInteraction(Interactable interactable) => Show($"Press {interactable.keyToPress.ToString()} to {interactable.description}");

    public static void Show(string message)
    {
        Instance.displayText.text = message;
        Instance.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.displayText.text = "";
        Instance.gameObject.SetActive(false);
    }
}
