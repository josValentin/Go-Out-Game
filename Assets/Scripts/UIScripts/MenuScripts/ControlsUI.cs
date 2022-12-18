using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public static ControlsUI Instance;
    [SerializeField] private Canvas canvas;
    [SerializeField] private UnityEngine.UI.Button btnClose;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Hide();
        btnClose.onClick.AddListener(Hide);

    }

    public static void Show() => Instance.canvas.enabled = true;
    public static void Hide() => Instance.canvas.enabled = false;
}
