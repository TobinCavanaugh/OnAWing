using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class MapGen : MonoBehaviour
{
    public SplineComputer splineComputer;

    public float step = 1f;

    public List<GameObject> islandPrefabs;

    public float maxOffset = 5f;

    public float yOffset = -10f;

    [Button]
    public void GenerateMap()
    {
        var parent = new GameObject("Map Parent").transform;
        
        for (float i = 0; i < 1f; i += step)
        {
            var t = (EditorUtility.InstantiatePrefab(islandPrefabs[Random.Range(0, islandPrefabs.Count - 1)]) as GameObject).transform;
            t.parent = parent;
            t.position = splineComputer.EvaluatePosition(i) + RandomOffset();
        }
            
    }

    private Vector3 RandomOffset()
    {
        return new Vector3(Random.Range(-maxOffset, maxOffset), yOffset, Random.Range(-maxOffset, maxOffset));
    }
}
