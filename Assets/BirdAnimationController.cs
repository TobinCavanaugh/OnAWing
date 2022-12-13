using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class BirdAnimationController : MonoBehaviour
{
    public Material material;
    public bool wingInPosition = false;

    [ColorUsage(true, true)] 
    public Color defaultColor;
    
    [ColorUsage(true, true)] 
    public Color chargedColor;

    public float colorLerpSpeed = 1.0f;

    public void WingUp()
    {
        wingInPosition = true;
    }

    public void WingDown()
    {
        wingInPosition = false;
    }

    [SerializeField, ReadOnly]
    private bool isCharging = false;

    [SerializeField, ReadOnly]
    private Color targetColor;
    private void Update()
    {
        var t = defaultColor;
        if (isCharging)
        {
            t = chargedColor;
        }
        else
        {
            t = defaultColor;
        }

        targetColor = Color.Lerp(targetColor, t, Time.deltaTime * colorLerpSpeed);
        material.SetColor("_BaseColor", targetColor);
        material.SetColor("_EmissionColor", targetColor);
        
        //material.SetColor(Property,
        //         Color.Lerp(material.GetColor(Property), lerpColor, Time.deltaTime * colorLerpSpeed));
    }


    public void StartCharge()
    {
        isCharging = true;
    }

    public void EndCharge()
    {
        isCharging = false;
    }
}
