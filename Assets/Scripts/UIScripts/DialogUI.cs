using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogUI : MonoBehaviour
{
    public static DialogUI Instance;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtDialog;
    [SerializeField] private Button btnNext;
    string[] dialogs;
    int index;
    bool isDialogCompleted;
    Action OnFullCompleted;

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
        btnNext.onClick.AddListener(Next);
    }

    public static void Show(string title, string[] dialogs, Action OnFullCompleted = null) => Instance._Show(title, dialogs, OnFullCompleted);
    public static void Hide() => Instance.canvas.enabled = false;

    private void Update()
    {
        if (!canvas.enabled) return;

        if (PlayerController.Instance.controls.Player.Jump.WasPressedThisFrame())
        {
            Next();
        }
    }

    private void _Show(string title, string[] dialogs, Action OnFullCompleted = null)
    {
        canvas.enabled = true;
        txtTitle.text = title;
        txtDialog.text = "";
        EnableNextButton(false);
        this.dialogs = dialogs;
        this.OnFullCompleted = OnFullCompleted;
        isDialogCompleted = false;
        index = 0;
        DisplayDialog(dialogs != null && dialogs.Length > 0 ? dialogs[index] : "...");
    }

    void DisplayDialog(string dialog)
    {
        StopCoroutine("IDiaplayDialog");
        StartCoroutine(IDiaplayDialog(dialog));
    }

    IEnumerator IDiaplayDialog(string dialog)
    {
        txtDialog.text = "";
        EnableNextButton(false);
        isDialogCompleted = false;
        for (int i = 0; i < dialog.Length; i++)
        {
            yield return null;
            if (isDialogCompleted)
            {
                txtDialog.text = dialog;
                EnableNextButton(true);
                yield break;
            }
            txtDialog.text += dialog[i];
        }
        isDialogCompleted = true;
        EnableNextButton(true);
    }

    void CompleteDialog()
    {
        isDialogCompleted = true;
    }

    private void Next()
    {
        if (dialogs == null || dialogs.Length == 0)
        {
            OnFullCompleted?.Invoke();
            Hide();
            return;
        }
        if (!isDialogCompleted)
        {
            CompleteDialog();
            return;
        }
        if (index + 1 == dialogs.Length)
        {
            OnFullCompleted?.Invoke();
            Hide();
            return;
        }
        index++;
        DisplayDialog(dialogs[index]);
    }

    void EnableNextButton(bool value) => btnNext.gameObject.SetActive(value);
}
