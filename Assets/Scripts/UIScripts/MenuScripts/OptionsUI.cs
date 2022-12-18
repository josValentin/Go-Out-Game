using UnityEngine;
using UnityEngine.UI;

public static class Options
{
    public static float GameVolume { get => AudioListener.volume; set => AudioListener.volume = value; }
    public static float CameraSensitivity { get; set; }
}

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button btnClose;
    [SerializeField] private Slider sliderGameVolume;
    [SerializeField] private Slider sliderCameraSensitivity;
    [SerializeField] private Button btnAccept;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
        Hide();
    }

    void Initialize()
    {
        sliderGameVolume.value = Options.GameVolume = PlayerPrefs.GetFloat("game_volume", 1f);
        sliderCameraSensitivity.value = Options.CameraSensitivity = PlayerPrefs.GetFloat("camera_sensitivity", 0.5f);

        btnAccept.onClick.AddListener(() =>
        {
            Options.GameVolume = sliderGameVolume.value;
            Options.CameraSensitivity = sliderCameraSensitivity.value;

            PlayerPrefs.SetFloat("game_volume", Options.GameVolume);
            PlayerPrefs.SetFloat("camera_sensitivity", Options.CameraSensitivity);

            PlayerPrefs.Save();

            Hide();
        });

        btnClose.onClick.AddListener(Hide);
    }

    public static void Show() => Instance.canvas.enabled = true;
    public static void Hide() => Instance.canvas.enabled = false;
}
