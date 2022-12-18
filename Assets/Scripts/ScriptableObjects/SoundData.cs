using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "Data/Sound Data")]
public class SoundData : ScriptableObject
{
    public SData[] list;

    public AudioClip GetClip(string id)
    {
        foreach (var item in list)
        {
            if (item.id.Equals(id))
                return item.clip;
        }
        return null;
    }
}

[System.Serializable]
public class SData
{
    public string id;
    public AudioClip clip;
}
