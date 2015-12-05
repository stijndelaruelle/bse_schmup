using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Schmup
{
    public class GlobalGameManager : Singleton<GlobalGameManager>
    {
        [SerializeField]
        private List<PlayerController> m_Players;
        private int m_DeathPlayers;

        private event Action m_GameStartEvent;
        public Action GameStartEvent
        {
            get { return m_GameStartEvent; }
            set { m_GameStartEvent = value; }

        }

        private event Action m_GameResetEvent;
        public Action GameResetEvent
        {
            get { return m_GameResetEvent; }
            set { m_GameResetEvent = value; }

        }

        private event Action m_GameOverEvent;
        public Action GameOverEvent
        {
            get { return m_GameOverEvent; }
            set { m_GameOverEvent = value; }
        }

        private void Start()
        {
            foreach(PlayerController player in m_Players)
            {
                player.DeathEvent += OnPlayerDeath;
            }

            StartGame();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (PlayerController player in m_Players)
            {
                player.DeathEvent -= OnPlayerDeath;
            }
        }

        private void OnPlayerDeath()
        {
            m_DeathPlayers += 1;

            if (m_DeathPlayers >= m_Players.Count)
            {
                GameOver();
            }
        }

        private void StartGame()
        {
            if (m_GameStartEvent != null)
                m_GameStartEvent();

            StopAllCoroutines();
            Time.timeScale = 1.0f;
        }

        private void GameOver()
        {
            if (m_GameOverEvent != null)
                m_GameOverEvent();

            StartCoroutine(GameOverSlowMotionRoutine());
        }

        private IEnumerator GameOverSlowMotionRoutine()
        {
            float timeScale = 0.5f;

            while (timeScale > 0.0f)
            {
                timeScale -= Time.deltaTime;
                Time.timeScale = timeScale;
                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = 0.0f;
        }

        public void RestartGame()
        {
            //Reset everything
            if (m_GameResetEvent != null)
                m_GameResetEvent();

            StartGame();
        }
    }
}