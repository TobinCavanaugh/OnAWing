using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
    public class IslandLodGen : MonoBehaviour
    {
        [FormerlySerializedAs("lodPercentage")] [Range(0, 100)]
        public float culledPercentage = 85f;
        [Button]
        public void GenerateLOD()
        {
            if (TryGetComponent(out LODGroup lodGroup))
            {

                lodGroup.SetLODs(new LOD[]
                {
                    new LOD(culledPercentage/100f, new []{GetComponent<Renderer>()})
                });
            }
        }
    }
}
