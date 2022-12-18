using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class FeatherPickup : MonoBehaviour
    {
        public float pickupDist = .4f;
        private bool hasBeenPickedUp = false;
        private Tweener _tweener;
        public float lerpSpeed = 1f;
        private Transform target;

        public float pickupScaleTime = .7f;
        public AnimationCurve pickupScaleCurve;

        private void Start()
        {
            target = transform;
        }

        private void Update()
        {
            if (hasBeenPickedUp)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + target.forward, Time.deltaTime * lerpSpeed);


                if (Vector3.Distance(transform.position, target.position) < pickupDist)
                {
                    transform.parent = target.parent;
                
                    transform.DOScale(Vector3.zero, pickupScaleTime).SetEase(pickupScaleCurve).OnComplete(() =>
                    {
                        transform.gameObject.SetActive(false);
                    });
                }
            } 
        }

        public void Pickup(Transform player)
        {
            if (hasBeenPickedUp)
            {
                return;
            }

            target = player;
            
            hasBeenPickedUp = true;
        }
    }    
}

