using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [Serializable]
    public class Helper
    {
        [FoldoutGroup("Events")]
        public UnityEvent activateEvent;
        
        [FoldoutGroup("Events")]
        public UnityEvent deactivateEvent;
    
        public Helper()
        {
            Initialize();
        }
        
        public virtual void Initialize()
        {
            
        }
        
        public virtual void Activate()
        {
            activateEvent?.Invoke();    
        }
    
        public virtual void Deactivate()
        {
            deactivateEvent?.Invoke();   
        }
    }
    
    [Serializable]
    public class ScaleHelper : Helper
    {
        public Transform reference;
        public Vector3 oldScale = Vector3.one;
        public Vector3 newScale = new(1.5f, 1.5f, 1.5f);
        public float tweenTime = .2f;
        public ScaleHelper()
        {
            Initialize();
        }
    
        public override void Initialize()
        {
            base.Initialize();
        }
    
        public override void Activate()
        {
            base.Activate();
    
            reference.DOKill();
            Sequence s = DOTween.Sequence();
            s.Append(reference.DOScale(newScale, tweenTime));
        }
    
        public override void Deactivate()
        {
            base.Deactivate();
            
            reference.DOKill();
            Sequence s = DOTween.Sequence();
            s.Append(reference.DOScale(oldScale, tweenTime));
        }
    }
}