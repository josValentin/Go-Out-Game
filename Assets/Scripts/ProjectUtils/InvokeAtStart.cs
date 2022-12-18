using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeAtStart : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnStart;
    // Start is called before the first frame update
    void Start()
    {
        OnStart?.Invoke();
    }
}
