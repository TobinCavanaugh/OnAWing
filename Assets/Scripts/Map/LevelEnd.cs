using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    public int nextSceneId = 1;
    public Image uiBlocker;
    public float fadeTime = 2f;
    public float loadMapDelay = .5f;
    public AnimationCurve fadeCurve;
    public Color fadeColor = Color.cyan;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBody playerBody))
        {
            uiBlocker.DOColor(fadeColor, fadeTime).OnComplete(() => StartCoroutine(WaitThenLoad())).SetEase(fadeCurve);
            
        }
    }

    IEnumerator WaitThenLoad()
    {
        yield return new WaitForSeconds(loadMapDelay);
        SceneManager.LoadScene(nextSceneId);
    }
}