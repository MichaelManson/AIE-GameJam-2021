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

        public void LoadMainMenu()
        {
            _scene.LoadScene(sceneBuildIndex: 0);
            
        }

        public async void LoadLobby()
        {
            // Make sure game is running at normal speed
            Time.timeScale = 1f;
            
            // Do the cool transition
            await GameManager.Instance.DoLevelTransitionAnimation();
            
            await Task.Delay(500);

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

