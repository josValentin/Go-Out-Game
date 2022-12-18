using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PlayableDirectorListeners : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPlayed;
    [SerializeField] private UnityEvent OnPaused;
    [SerializeField] private UnityEvent OnStopped;

    private void Awake()
    {
        PlayableDirector director = GetComponent<PlayableDirector>();

        director.played += (PlayableDirector _director) => OnPlayed?.Invoke();
        director.paused += (PlayableDirector _director) => OnPaused?.Invoke();
        director.stopped += (PlayableDirector _director) => OnStopped?.Invoke();
    }
}
