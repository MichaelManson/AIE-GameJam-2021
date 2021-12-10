using System;
using System.Collections;
using System.Collections.Generic;
using _scene = UnityEngine.SceneManagement.SceneManager;
using UnityEngine;
// ReSharper disable CheckNamespace

namespace CookieClash
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadMainMenu()
        {
            _scene.LoadScene(sceneBuildIndex: 0);
            
        }

        public void LoadLobby()
        {
            _scene.LoadScene(sceneBuildIndex: 1);

        }

        public void LoadGame()
        {
            _scene.LoadScene(sceneBuildIndex: 2);

        }

        public void LoadEndGame()
        {
            _scene.LoadScene(sceneBuildIndex: 3);

        }
    }
}

