using System;
using DG.Tweening;
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

    public float colorChangeTime = 1.5f;
    private static readonly int Property = Shader.PropertyToID("_BaseColor");

    public void WingUp()
    {
        wingInPosition = true;
    }

    public void WingDown()
    {
        wingInPosition = false;
    }

    private Color colorVal;
    private Tweener tweener;
    private void Start()
    {
        tweener = DOVirtual.Color(material.GetColor(Property), colorVal, colorChangeTime, x =>
        {
            material.SetColor(Property, x);
        });        
    }


    public void StartCharge()
    {
        colorVal = chargedColor;
        tweener.Play();
        tweener.Restart();
    }

    public void EndCharge()
    {
        colorVal = defaultColor;
        tweener.Play();
        tweener.Restart();
    }
}
