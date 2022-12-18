using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepsSound : MonoBehaviour
{
    public static StepsSound Instance;
    private AudioSource source;

    private static bool IsPlaying;
    private static float Spacing;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            Instance.gameObject.SetActive(false);
        }
        //return;
        source = GetComponent<AudioSource>();
        Instance = this;
        //PlaySteps(1);
    }

    public static void PlaySteps(float spacing)
    {
        IsPlaying = true;

        //StopSteps();
        //Instance.StartCoroutine(Instance.CPlaySteps(spacing));
        if (spacing != Spacing)
        {
            Spacing = spacing;
            Instance.time = spacing;
            Instance.source.Play();
        }

    }

    public static void StopSteps()
    {
        IsPlaying = false;
        Instance.source.Stop();
        Spacing = 0;
        //Instance.StopCoroutine("CPlaySteps");
    }

    void Update()
    {
        if (!PlayerController.ControlEnabled) return;
        if (!IsPlaying) return;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = Spacing;
            source.Play();
        }
    }

    //IEnumerator CPlaySteps(float spacing)
    //{
    //    source.Play();
    //    yield return new WaitForSeconds(spacing);
    //    yield return CPlaySteps(spacing);
    //}
}
