using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public SkinnedMeshRenderer birdRenderer;
        public Material invisibleMaterial;
        public Material wingMaterial;
        public SplineBasedBirdController splineBasedBirdController;

        public int maxFeathers = 6;
        
        [SerializeField, ReadOnly]
        private int curFeathers = 0;

        public static PlayerManager instance;

        public float featherSpeedIncrease = 5f;
        
        [Header("PauseMenu")]
        public GameObject pauseMenu;
        public ButtonHelper[] buttonHelpers;
        

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Duplicate of the playermanager");
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
        }

        public float timeLerpTime = .5f;
        private Tweener _tweener;
        public void SetTimeScale(float newTime)
        {
            //Returning to regular timescale
            if (newTime > 0)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }

            buttonHelpers.ForEach(b => b.PointerExit());
            
            _tweener?.Kill();

            _tweener = DOVirtual.Float(Time.timeScale, newTime, timeLerpTime, x =>
            {
                Time.timeScale = x;
            });
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Full speed
                if (Time.timeScale > .5f)
                {
                    SetTimeScale(0);
                }
                else
                {
                    SetTimeScale(1);
                }
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
            splineBasedBirdController.defaultSpeed += featherSpeedIncrease;
        }
    }
}
