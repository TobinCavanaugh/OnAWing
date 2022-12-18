using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(BoxCollider)), ExecuteInEditMode]
    public class IslandGen : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        public BoxCollider boxCol;
        public float raycastDistance = 50f;

        public LayerMask mask;
        public int grassCount = 100;
        public int prefabCount = 25;
        
        void Reset(){
            boxCol = GetComponent<BoxCollider>();    
        }

        [AssetsOnly]
        public GameObject[] prefabs;
        
        [AssetsOnly]
        public GameObject grassPrefab;

        public bool selectIsland = false;
        
        private List<GameObject> placedPrefabs = new();
        
        
        [Button()]
        public void GenerateIsland()
        {
            placedPrefabs = new();
            
            Transform island = null;
            var propParent = new GameObject("PROP PARENT").transform;
            var grassParent = new GameObject("GRASS PARENT").transform;

            //Find the island
            if (Physics.Raycast(transform.position, Vector3.down, out var hit,
                    raycastDistance, mask))
            {
                island = hit.transform;
                propParent.parent = island;
                grassParent.parent = island;
                island.name += $" - gen ({Random.Range(0, 32767)})";
                placedPrefabs.Add(propParent.gameObject);
                placedPrefabs.Add(grassParent.gameObject);
            }
            else
            {            
                Destroy(propParent.gameObject);
                Destroy(grassParent.gameObject);
                Debug.LogError("Couldnt find island, make sure island has a collider and is on the island layer");
                return;
            }


            //Place the grass
            for (int g = 0; g < grassCount; g++)
            {
                var grass = (EditorUtility.InstantiatePrefab(grassPrefab) as GameObject).transform;
                grass.SetPositionAndRotation(GetPosition(), grassPrefab.transform.rotation);
                grass.transform.parent = grassParent;
                placedPrefabs.Add(grass.gameObject);
            }

            //Place the prefabs
            for (int p = 0; p < prefabCount; p++)
            {
                var prefab = prefabs[Random.Range(0, prefabs.Length - 1)];
                var prop = (EditorUtility.InstantiatePrefab(prefab) as GameObject).transform;
                prop.SetPositionAndRotation(GetPosition(), prefab.transform.rotation);
                prop.transform.parent = propParent;
                placedPrefabs.Add(prop.gameObject);
            }
            
            if (selectIsland)
            {
                Selection.objects = new []{island.gameObject};
            }

            canUndo = true;
        }

        private bool canUndo = false;
        
        [ShowIf(nameof(canUndo))]
        
        [Button("Undo")]
        public void Undo()
        {
            canUndo = false;

            placedPrefabs.RemoveAll(x => x == null);

            placedPrefabs.ForEach(DestroyImmediate);
        }

        /// <summary>
        /// Returns a raycast hit point on the island
        /// </summary>
        /// <returns></returns>
        private Vector3 GetPosition()
        {
            var bounds = boxCol.bounds;

            float xRand = Random.Range(bounds.min.x, bounds.max.x);
            float zRand = Random.Range(bounds.min.z, bounds.max.z);

            if (Physics.Raycast(new Vector3(xRand, transform.position.y, zRand), Vector3.down, out var hit,
                    raycastDistance, mask))
            {
                return hit.point;
            }

            return GetPosition();
        }
        
        #endif
    }
}
