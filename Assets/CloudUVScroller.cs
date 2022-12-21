using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using LinqExtensions = Sirenix.Utilities.LinqExtensions;

public class CloudUVScroller : MonoBehaviour
{
    public Material cloudMat;

    public float addAmount = .001f;
    public float lerpSpeed = .01f;
    private float curAmount = 0;
    private void Update()
    {
        curAmount += addAmount;
        //cloudMat.SetTextureOffset("_MainTex", new(curAmount, 0));
        cloudMat.mainTextureOffset = new(curAmount, 0);
        //cloudMat.SetVector("_Offset", new Vector4(curAmount, 0));    
    }
}
