using UnityEngine;
using ProjectUtils;
using UnityEngine.Events;
using ReachableGames.PostLinerFree;

public class InteractableSpeaker : Interactable
{
    [System.Serializable]
    public class IDialog
    {
        public string[] dialogs;
        public UnityEvent OnCompleted;
    }
    public enum State
    {
        Default,
        Waiting,
        Finished
    }

    public ItemData itemRequired;
    public int amountRequired = 1;
    public bool devMode = false;
    private State state;

    [SerializeField] private string title;
    [SerializeField] private IDialog FirstDialogs;
    [SerializeField] private IDialog WaitingDialogs;
    [SerializeField] private IDialog FinalDialogs;

    [SerializeField] private ItemData rewardItem;
    [SerializeField] private bool showInstruction = false;
    public override void Evaluate(ItemInHand itemInHand)
    {
        if (showInstruction)
        {
            ShortPopupUI.Show("Press Space to continue the conversation");
            showInstruction = false;
        }
        PlayerController.SetControl(false);
        SoundManager.PlayOneShot("greet", 0.65f);

        PostLinerOutline outline = GetComponent<PostLinerOutline>();
        if (outline != null) outline.enabled = false;

        switch (state)
        {
            case State.Default:

                DialogUI.Show(title, FirstDialogs.dialogs, () =>
                {
                    if (outline != null) outline.enabled = true;

                    FirstDialogs.OnCompleted?.Invoke();
                    this.ActionAfterReturnedNull(() =>
                    {
                        PlayerController.SetControl(true);
                    });
                    state = State.Waiting;
                });
                break;
            case State.Waiting:
#if UNITY_EDITOR
                if (devMode)
                {
                    SuccessInteraction();
                    return;
                }
#endif
                if (itemRequired == null || amountRequired == 0)
                {
                    SuccessInteraction();
                    return;
                }

                if (itemInHand == null || itemRequired != itemInHand.data)
                {
                    FailInteraction();
                    return;
                }

                InventoryItem inventoryItem = Inventory.Get(itemRequired);

                if (inventoryItem.stackSize >= amountRequired)
                {
                    Inventory.Remove(itemRequired, amountRequired);
                    SuccessInteraction();
                    //
                }
                else
                {
                    FailInteraction();
                }

                break;
            case State.Finished:

                break;
            default:
                break;
        }

    }

    void FailInteraction()
    {
        DialogUI.Show(title, WaitingDialogs.dialogs, () =>
        {
            WaitingDialogs.OnCompleted?.Invoke();
            this.ActionAfterReturnedNull(() =>
            {
                PlayerController.SetControl(true);

                PostLinerOutline outline = GetComponent<PostLinerOutline>();
                if (outline != null) outline.enabled = true;

            });
        });
    }

    void SuccessInteraction()
    {
        DialogUI.Show(title, FinalDialogs.dialogs, () =>
        {
            FinalDialogs.OnCompleted?.Invoke();
            if (rewardItem != null) Inventory.Add(rewardItem);
            this.ActionAfterReturnedNull(() =>
            {
                PlayerController.SetControl(true);
            });
            state = State.Finished;

        });
        Clear();
    }

    private void Clear()
    {
        gameObject.layer = 0;
    }
}
