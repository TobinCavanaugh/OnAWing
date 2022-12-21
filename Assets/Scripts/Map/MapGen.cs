using System.Collections.Generic;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Map
{
    [ExecuteInEditMode]
    public class MapGen : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        public SplineComputer splineComputer;
    
        public float step = 1f;
    
        public List<GameObject> islandPrefabs = new();
    
        public float maxOffset = 5f;
    
        public float yOffset = -10f;
    
        [Space]
    
        public List<GameObject> cloudPrefabs;
        public float cloudMaxOffset = 50f;
        public float cloudMinOffset = 10f;
        public float cloudStep = .1f;

        public bool generateIslands = true;
        public bool generateClouds = true;
    
        [Button]
        public void GenerateMap()
        {
            var parent = new GameObject("Map Parent").transform;
            var cloudParent = new GameObject("Cloud Parent").transform;

            if (generateIslands)
            {
                for (double i = 0; i < 1f; i += step)
                {
                    var t = (PrefabUtility.InstantiatePrefab(islandPrefabs[Random.Range(0, islandPrefabs.Count - 1)])as GameObject).transform;
                    t.parent = parent;
                    t.position = splineComputer.EvaluatePosition(i) + RandomOffset();
                }    
            }

            if (generateClouds)
            {
                for (double c = 0; c < 1f; c += cloudStep)
                {
                    var t = (PrefabUtility.InstantiatePrefab(cloudPrefabs[Random.Range(0, cloudPrefabs.Count - 1)])as GameObject).transform; 
                    t.parent = cloudParent;
                    t.position = splineComputer.EvaluatePosition(c) + CloudOffset();

                    var te = t.eulerAngles;
                    t.eulerAngles = new(te.x, Random.Range(-360, 360), te.z);
                }
            }
        }
    
        private Vector3 CloudOffset()
        {
            Debug.Log(GetRandMult());
            return new Vector3(GetRandMult() * Random.Range(cloudMinOffset, cloudMaxOffset),
                GetRandMult() * Random.Range(cloudMinOffset, cloudMaxOffset),
                GetRandMult() * Random.Range(cloudMinOffset, cloudMaxOffset));
        }
    
        private float GetRandMult()
        {
            var iNum = Random.Range(-50, 50);
            if (iNum == 0)
            {
                iNum++;
            }
            return iNum / (int)Mathf.Abs(iNum);
        }
    
        private Vector3 RandomOffset()
        {
            return new Vector3(Random.Range(-maxOffset, maxOffset), yOffset, Random.Range(-maxOffset, maxOffset));
        }
        
        #endif
    }
}
