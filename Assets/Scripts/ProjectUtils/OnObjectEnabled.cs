using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnObjectEnabled : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnEnabled;
    [SerializeField] private UnityEngine.Events.UnityEvent OnDisabled;
    private void OnEnable()
    {
        OnEnabled?.Invoke();
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke();
    }
}
