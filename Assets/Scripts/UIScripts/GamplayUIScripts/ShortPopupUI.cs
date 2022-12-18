using UnityEngine;
using DG.Tweening;
using TMPro;

public class ShortPopupUI : MonoBehaviour
{
    public static ShortPopupUI Instance;

    [SerializeField] private TextMeshProUGUI text;
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    bool isPlaying;

    void Start()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        rect = transform as RectTransform;

        gameObject.SetActive(false);
    }

    public static void ShowFailedInteraction(InteractableObject interactable)
    {
        string itemName = interactable.itemRequired.itemName;
        int amount = interactable.amountRequired;

        if (amount > 1)
            Show($"You need {amount} {interactable.itemRequired.itemName}(s)");
        else
            Show($"You need a {interactable.itemRequired.itemName}");
    }

    public static void Show(string message) => Instance._Show(message);

    private void _Show(string message)
    {
        if (isPlaying) return;
        DOTween.Kill(canvasGroup);
        isPlaying = true;

        ResetUI();
        text.text = message;
        gameObject.SetActive(true);

        canvasGroup.DOFade(1, 0.5f);
        rect.DOAnchorPosY(-50, 0.5f).OnComplete(() =>
        {
            isPlaying = false;
            canvasGroup.DOFade(0, 0.5f).SetDelay(0.85f).OnComplete(() =>
            {
                if (!isPlaying)
                    gameObject.SetActive(false);
            });
        });
    }

    private void ResetUI()
    {
        Instance.canvasGroup.alpha = 0;

        Vector2 pos = rect.anchoredPosition;
        pos.y = -100;
        rect.anchoredPosition = pos;
    }
}
