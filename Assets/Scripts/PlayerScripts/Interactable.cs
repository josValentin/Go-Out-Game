using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.E;
    public string description;

    public virtual void Evaluate(ItemInHand itemInHand) { }
}
