using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioSourceHelper : MonoBehaviour
{
    [Button]
    public void Play()
    {
        GetComponent<AudioSource>().Play();
    }
}
