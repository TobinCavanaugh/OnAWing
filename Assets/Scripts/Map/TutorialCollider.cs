using DG.Tweening;
using Player;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public GameObject uiElement;

    public float time = .2f;

    public Tweener tweener;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBody playerBody))
        {
            uiElement.transform.localScale = Vector3.zero;
            uiElement.transform.DOScale(Vector3.one, time);
            uiElement.SetActive(true);

            tweener = DOVirtual.Float(Time.timeScale, .05f, time, value =>
            {
                Time.timeScale = value;
            });
        }
    }
}
