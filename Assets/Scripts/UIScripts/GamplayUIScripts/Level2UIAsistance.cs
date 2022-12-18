using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2UIAsistance : MonoBehaviour
{
    public static void ShowPopup(string message) => ShortPopupUI.Show(message);
    public static void ShowPopup(InteractableObject interactable) => ShortPopupUI.ShowFailedInteraction(interactable);

    public static void AddObjective(string text) => ObjectivesUI.AddObjective(text, text);
    public static void CompleteObjective(string text) => ObjectivesUI.CompleteObjective(text);

}
