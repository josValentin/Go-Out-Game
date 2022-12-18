using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private UnityEvent OnEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            OnEnter?.Invoke();
            Destroy(gameObject);
        }
    }
}
