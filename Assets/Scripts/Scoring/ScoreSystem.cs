using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Schmup
{
    public class ScoreSystem : MonoBehaviour
    {
        public enum ScoreType
        {
            TotalScore,
            CurrentScore,
            Multiplier
        }

        private IScoreable[] m_Scoreables;

        private int m_TotalScore;
        private int m_CurrentScore; //Score during the current streak
        private int m_Multiplier;

        private Action<int> m_TotalScoreUpdateEvent;
        public Action<int> TotalScoreUpdateEvent
        {
            get { return m_TotalScoreUpdateEvent; }
            set { m_TotalScoreUpdateEvent = value; }
        }

        private Action<int> m_CurrentScoreUpdateEvent;
        public Action<int> CurrentScoreUpdateEvent
        {
            get { return m_CurrentScoreUpdateEvent; }
            set { m_CurrentScoreUpdateEvent = value; }
        }

        private Action<int> m_MultiplierUpdateEvent;
        public Action<int> MultiplierUpdateEvent
        {
            get { return m_MultiplierUpdateEvent; }
            set { m_MultiplierUpdateEvent = value; }
        }

        private void Start()
        {
            GlobalGameManager.Instance.GameResetEvent += OnGameReset;

            //Super duper lame, fix!
            m_Scoreables = GameObject.FindObjectsOfType<DamageableObject>();
            

            foreach (IScoreable scoreable in m_Scoreables)
            {
                scoreable.ScoreEvent += OnScore;
            }
        }

        private void OnDestroy()
        {
            GlobalGameManager.Instance.GameResetEvent -= OnGameReset;

            foreach (IScoreable scoreable in m_Scoreables)
            {
                scoreable.ScoreEvent -= OnScore;
            }
        }

        private void OnScore(int score)
        {
            m_TotalScore += score;

            if (m_TotalScoreUpdateEvent != null)
                m_TotalScoreUpdateEvent(m_TotalScore);
        }

        private void OnGameReset()
        {
            m_TotalScore = 0;
            m_CurrentScore = 0;
            m_Multiplier = 0;

            if (m_TotalScoreUpdateEvent != null)
                m_TotalScoreUpdateEvent(m_TotalScore);

            if (m_CurrentScoreUpdateEvent != null)
                m_CurrentScoreUpdateEvent(m_TotalScore);

            if (m_MultiplierUpdateEvent != null)
                m_MultiplierUpdateEvent(m_TotalScore);
        }
    }
}