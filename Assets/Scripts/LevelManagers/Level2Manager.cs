using UnityEngine;
using ProjectUtils;
using ReachableGames.PostLinerFree;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;
    [SerializeField] private GameObject[] toEnableAfterTransition;
    [SerializeField] private GameObject[] toEnableDuringTransition;

    public void StartFinishGame()
    {
        this.ActionAfterTime(13f, () => ChangeSceneUI.ChangeScene("Final"));
    }

    private void Awake() => Instance = this;

    public static void OnTransitionFinished()
    {
        foreach (var item in Instance.toEnableAfterTransition) item.gameObject.SetActive(true);
        PlayerController.SetControl(true);
    }

    private void Start()
    {
        PlayerStateUI.RespawnTarget = "Level 2";

        this.ActionAfterTime(1.2f, () => ObjectivesUI.Show(() => ObjectivesUI.AddObjective("Find a way to scape the zone")));

        if (!Level1Manager.LevelTransition) return;

        foreach (var obj in toEnableDuringTransition) obj.gameObject.SetActive(false);
        foreach (var item in Instance.toEnableAfterTransition) item.gameObject.SetActive(false);

        this.ActionAfterTime(5.6f, () =>
        {
            foreach (var obj in toEnableDuringTransition) obj.gameObject.SetActive(true);

        });


    }
}
