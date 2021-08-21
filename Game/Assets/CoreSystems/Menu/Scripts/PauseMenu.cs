using CoreSystems.Transition.Scripts;
using UnityEngine;

namespace CoreSystems.Menu.Scripts
{
    public class PauseMenu : MonoBehaviour, IPauseMenu
    {
        public bool IsPaused { get; private set; }

        private GameObject _canvas;

        void Awake()
        {
            _canvas = transform.GetChild(0).gameObject;
        }

        void Start()
        {
            UnpauseGame();
        }

        public void Toggle()
        {
            if (IsPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            IsPaused = true;
            _canvas.SetActive(true);
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
            IsPaused = false;
            _canvas.SetActive(false);
        }

        public void Back()
        {
            UnpauseGame();
            LevelLoader.Instance.LoadLevel(Level.Menu);
        }
    }
}
