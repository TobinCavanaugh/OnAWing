using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{
    public void Resume()
    {
        var tcs = GameObject.FindObjectsOfType<TutorialCollider>();
        tcs.ForEach(x => x.tweener.Kill());
        
        Time.timeScale = 1f;
    }
}
