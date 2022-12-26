using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PageManager : MonoBehaviour
    {
        public List<GameObject> pages;
        public List<ButtonHelper> buttonHelpers;

        private void LateUpdate()
        {
            Time.timeScale = 1f;
        }

        public void ResetButtonHelpers()
        {
            buttonHelpers.ForEach(x =>
            {
                x.PointerExit();
            });
        }

        public void SetPage(int pageNum)
        {
            pages.ForEach(x => x.SetActive(false));
            pages[pageNum].SetActive(true);
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}