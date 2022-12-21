using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RawImage))]
    public class RawImageVideoRenderer : MonoBehaviour
    {
        public List<Texture2D> textures;

        private RawImage _rawImage;

        public int fps = 60;

        private bool playing = true;

        public bool autoLoop = true;

        private WaitForSeconds _waitForSeconds;
        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(1f/fps);
            _rawImage = GetComponent<RawImage>();
            StartCoroutine(Tick(autoLoop));
        }

        [Button]
        public void PlayOneShot()
        {
            StopAllCoroutines();
            index = 0;
            playing = true;
            StartCoroutine(Tick(false));
        }

        [Button]
        public void PlayLooping()
        {
            StopAllCoroutines();
            index = 0;
            playing = true;
            StartCoroutine(Tick(true));
        }
        
        [Button]
        public void Stop()
        {
            StopAllCoroutines();
            playing = false;
        }
        
        [Button]
        public void Resume(bool looping = true)
        {
            playing = true;
            StartCoroutine(Tick(looping));
        }

        private int index = 0;
        private IEnumerator Tick(bool loop = true)
        {
            if (!playing)
            {
                yield return null;
            }
        
            _rawImage.texture = textures[index];
            index++;

            if (index >= textures.Count)
            {
                if (!loop)
                {
                    Stop();
                }
                
                index = 0;
            }
        
            yield return _waitForSeconds;
        
            StartCoroutine(Tick(loop));
        }
    }
}