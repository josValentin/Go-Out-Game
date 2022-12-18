using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class ChangeSceneUI : MonoBehaviour
{
    private static ChangeSceneUI Instance;
    [SerializeField] private Image fadeImage;

    public static void ChangeScene(string sceneName, Action OnCompleteFade = null) => Instance._ChangeScene(sceneName, OnCompleteFade);
    public static void ChangeScene(string sceneName) => Instance._ChangeScene(sceneName);
    public static void ChangeSceneAdditive(string sceneName, Action OnCompleteFade = null, Action afterSceneLoad = null) => Instance._ChangeSceneAdditive(sceneName, OnCompleteFade,afterSceneLoad);
    public static void ChangeSceneAdditive(string sceneName) => Instance._ChangeSceneAdditive(sceneName);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LetShowScene();
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => LetShowScene();
    }

    private void LetShowScene()
    {
        Time.timeScale = 1;
        fadeImage.color = Color.black;
        fadeImage.DOFade(0, 1f).SetDelay(0.1f);
    }

    private void _ChangeScene(string sceneName, Action OnCompleteFade = null)
    {
        fadeImage.DOFade(1, 1f).SetUpdate(true).OnComplete(() =>
        {
            OnCompleteFade?.Invoke();
            SceneManager.LoadScene(sceneName);
        });
    }

    private void _ChangeSceneAdditive(string sceneName, Action OnCompleteFade = null, Action afterSceneLoad = null)
    {
        fadeImage.DOFade(1, 1f).SetUpdate(true).OnComplete(() =>
        {
            OnCompleteFade?.Invoke();
            SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
            afterSceneLoad?.Invoke();
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        });
    }
}
