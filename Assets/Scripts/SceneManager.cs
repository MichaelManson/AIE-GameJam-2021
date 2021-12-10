using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async void LoadMainMenu()
        {
            // Do the cool transition
            await GameManager.Instance.DoLevelTransitionAnimation();
            
            _scene.LoadScene(sceneBuildIndex: 0);
        }

        public async void LoadLobby()
        {
            // Do the cool transition
            await GameManager.Instance.DoLevelTransitionAnimation();

            _scene.LoadScene(sceneBuildIndex: 1);
        }

        public async void LoadGame()
        {
            // Do the cool transition
            await GameManager.Instance.DoLevelTransitionAnimation();
            
            _scene.LoadScene(sceneBuildIndex: 2);
        }

        public async void LoadEndGame()
        {
            // Do the cool transition
            await GameManager.Instance.DoLevelTransitionAnimation();
            
            _scene.LoadScene(sceneBuildIndex: 3);
        }
    }
}

