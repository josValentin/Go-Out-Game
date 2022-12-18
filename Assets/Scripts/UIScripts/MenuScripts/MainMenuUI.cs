using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnControls;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnExit;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        btnPlay.onClick.AddListener(() =>
        {
            ChangeSceneUI.ChangeScene("Level 1", () =>
            {
                OptionsUI.Hide();
                ControlsUI.Hide();
            });

        });
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
        btnExit.onClick.AddListener(Application.Quit);
    }
}
