using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public SkinnedMeshRenderer birdRenderer;
        public Material invisibleMaterial;
        public Material wingMaterial;

        public int maxFeathers = 6;
        
        [SerializeField, ReadOnly]
        private int curFeathers = 0;

        public static PlayerManager instance;


        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Duplicate of the playermanager you fuckin troglodyte");
            }
            
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < maxFeathers; i++)
            {
                birdRenderer.materials[i] = invisibleMaterial;
            }
            
            try
            {
                DontDestroyOnLoad(this.gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        
        public void AddFeather()
        {
            var mats = birdRenderer.materials;

            curFeathers++;
            for (int i = 0; i < curFeathers; i++)
            {
                mats[i] = wingMaterial;
            }

            birdRenderer.materials = mats;

        }
    }
}
