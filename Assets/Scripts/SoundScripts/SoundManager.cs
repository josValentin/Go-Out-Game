using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource OneShotSource;
    public SoundData data;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static void PlayOneShot(AudioClip clip, float volume = 1)
    {
        Instance.OneShotSource.volume = volume;
        Instance.OneShotSource.PlayOneShot(clip);
    }

    public static void PlayOneShot(string id, float volume = 1)
    {
        AudioClip clip = Instance.data.GetClip(id);
        if (clip == null) return;
        Instance.OneShotSource.volume = volume;
        Instance.OneShotSource.PlayOneShot(clip);
    }

    public static void PlayOneShot(string id) => PlayOneShot(id, 1);
}
