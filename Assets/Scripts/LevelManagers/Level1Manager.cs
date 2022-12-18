using UnityEngine;
using UnityEngine.Playables;
using ReachableGames.PostLinerFree;
public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;
    public static bool LevelTransition;
    [SerializeField] private PlayableDirector transitionSequence;
    [SerializeField] private GameObject[] toDisableAfterTransition;
    [SerializeField] private PostLinerRenderer lineRender;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }
    void Start()
    {
        PlayerStateUI.RespawnTarget = "Level 1";
        Inventory.items.Clear();
        GamplayInvetory.ClearBackup();
    }

    public void FirstObjective() => ObjectivesUI.Show(() => ObjectivesUI.AddObjective("Get out the zone", "Get out the zone"));

    public void LoadLevel2()
    {
        GamplayInvetory.SaveBackup();
        LevelTransition = true;
        ClearOutlineRender();
        ChangeSceneUI.ChangeSceneAdditive("Level 2", null, () => transitionSequence.Play());
    }

    void ClearOutlineRender()
    {
        lineRender.ClearAllOutlines();
        lineRender.enabled = false;
        PostLinerRenderer.Instance = null;
    }

    public static void OnTransitionFinished()
    {
        foreach (var item in Instance.toDisableAfterTransition) item.gameObject.SetActive(false);

        Level2Manager.OnTransitionFinished();
        LevelTransition = false;
    }
}
