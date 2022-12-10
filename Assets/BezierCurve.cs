using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] 
    public List<Transform> positions = new();

    
    
    public float resolutionStep = 5f;

    private void OnDrawGizmosSelected()
    {
        positions.ForEach(x =>
        {
            //Handles.Draw
        });
    }

    public void UpdateCurve()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            //positions[i].
        }    
    }
}

