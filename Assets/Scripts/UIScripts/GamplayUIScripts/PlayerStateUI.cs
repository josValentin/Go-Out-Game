using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ProjectUtils;

public class PlayerStateUI : MonoBehaviour
{
    public static PlayerStateUI Instance;
    public static string RespawnTarget;
    [SerializeField] private float startLife = 10;
    [SerializeField] private Image render;
    [SerializeField] private Slider slider;
    float _life;
    bool _critical;
    bool _damageAllowed = true;

    private void Start()
    {
        Instance = this;
        _life = startLife;
        slider.maxValue = startLife;
        slider.value = startLife;
    }

    public void RecoverLife(float amount = 1f)
    {
        _life += amount;
        if (_life > startLife) _life = startLife;
        SoundManager.PlayOneShot("recover");

        DoRender(Color.green, CalculateRenderStrength(amount), () => SetState(CalculateCritic()));
        slider.DOValue(_life, 0.3f);
    }

    public void TakeDamage(float amount = 1f)
    {
        if (!PlayerController.ControlEnabled) return;
        if (!_damageAllowed) return;
        SoundManager.PlayOneShot("damage");
        _damageAllowed = false;
        MouseLook.Instance.Shake();
        this.ActionAfterTime(0.5f, () => _damageAllowed = true);

        _life -= amount;

        if (_life <= 0)
        {
            _life = 0;
            Debug.Log("Dead");
            PlayerController.SetControl(false);
            MouseLook.Instance.Die();
            this.ActionAfterTime(1f, () =>
            {
                ChangeSceneUI.ChangeScene(RespawnTarget, () =>
                {
                    PlayerController.SetControl(true);
                });
            });
        }
        slider.DOValue(_life, 0.3f);
        DoRender(Color.red, CalculateRenderStrength(amount), () => SetState(CalculateCritic()));
    }

    float CalculateRenderStrength(float amount) => Mathf.InverseLerp(0, startLife / 8, amount);
    bool CalculateCritic() => (_life <= startLife / 8);

    public void SetState(bool critical)
    {
        if (this._critical == critical) return;
        if (critical)
        {
            SetColorWithClearAlpha(Color.red);
            render.DOFade(0.7f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            DOTween.Kill(render);
        }
        _critical = critical;
    }

    public void DoRender(Color color, float strength, Action OnComplete = null)
    {
        SetColorWithClearAlpha(color);
        render.DOFade(strength, 0.3f).OnComplete(() =>
        {
            render.DOFade(0, 0.3f).OnComplete(() =>
            {
                OnComplete?.Invoke();
            });
        });
    }

    void SetColorWithClearAlpha(Color color)
    {
        color.a = 0;
        render.color = color;
    }
}
