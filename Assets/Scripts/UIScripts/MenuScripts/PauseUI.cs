using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance;

    public static bool IsShown { get; set; }

    [SerializeField] private Canvas canvas;

    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnControls;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private Button btnExit;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Initialize();
        Hide(false);
    }

    void Initialize()
    {
        btnResume.onClick.AddListener(() => Hide());
        btnControls.onClick.AddListener(() =>
        {
            ControlsUI.Show();
            OptionsUI.Hide();
        });
        btnOptions.onClick.AddListener(() =>
        {
            OptionsUI.Show();
            ControlsUI.Hide();
        });

        btnMainMenu.onClick.AddListener(() => ChangeSceneUI.ChangeScene("Main Menu"));

        btnExit.onClick.AddListener(Application.Quit);
    }
    static bool wasControlDisabled = false;
    public static void Show()
    {
        IsShown = true;
        Instance.canvas.enabled = true;
        Time.timeScale = 0;

        if (!PlayerController.ControlEnabled)
            wasControlDisabled = true;
        else
            PlayerController.ControlEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public static void Hide(bool checkControl = true)
    {
        IsShown = false;
        Time.timeScale = 1;
        Instance.canvas.enabled = false;
        if (checkControl)
        {
            if (wasControlDisabled)
                wasControlDisabled = false;
            else
                PlayerController.ControlEnabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (OptionsUI.Instance != null) OptionsUI.Hide();

        if(ControlsUI.Instance != null) ControlsUI.Hide();
    }
}
