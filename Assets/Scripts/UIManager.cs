using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_HUDPanel;

        [SerializeField]
        private GameObject m_GameOverPanel;

        private GlobalGameManager m_GlobalGameManager;

        private void Start()
        {
            m_GlobalGameManager = GlobalGameManager.Instance;

            if (m_GlobalGameManager == null)
            {
                Debug.LogError("UIManager doesn't have a gamemanager reference!");
                return;
            }

            m_GlobalGameManager.GameStartEvent += OnGameStart;
            m_GlobalGameManager.GameOverEvent += OnGameOver;
        }

        private void OnDestroy()
        {
            if (m_GlobalGameManager == null)
                return;

            m_GlobalGameManager.GameStartEvent -= OnGameStart;
            m_GlobalGameManager.GameOverEvent -= OnGameOver;
        }

        private void OnGameStart()
        {
            m_HUDPanel.SetActive(true);
            m_GameOverPanel.SetActive(false);
        }

        private void OnGameOver()
        {
            m_HUDPanel.SetActive(false);
            m_GameOverPanel.SetActive(true);
        }

        public void RestartGame()
        {
            m_GlobalGameManager.RestartGame();
        }
    }
}