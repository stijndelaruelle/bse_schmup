using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Schmup
{
    public class ScoreLabel : MonoBehaviour
    {
        [SerializeField]
        private ScoreSystem m_ScoreSystem;

        [SerializeField]
        private ScoreSystem.ScoreType m_ScoreType;

        [SerializeField]
        private Text m_Text;
        private int m_TargetScore;
        private int m_DisplayedScore;

        private void Start()
        {
            if (m_ScoreSystem == null)
                Debug.LogError("ScoreLabel doesn't have a ScoreSystem reference!");

            switch (m_ScoreType)
            {
                case ScoreSystem.ScoreType.TotalScore:
                    m_ScoreSystem.TotalScoreUpdateEvent += OnScoreUpdate;
                    break;

                case ScoreSystem.ScoreType.CurrentScore:
                    m_ScoreSystem.CurrentScoreUpdateEvent += OnScoreUpdate;
                    break;

                case ScoreSystem.ScoreType.Multiplier:
                    m_ScoreSystem.MultiplierUpdateEvent += OnScoreUpdate;
                    break;

                default:
                    m_ScoreSystem.TotalScoreUpdateEvent += OnScoreUpdate;
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (m_ScoreType)
            {
                case ScoreSystem.ScoreType.TotalScore:
                    m_ScoreSystem.TotalScoreUpdateEvent -= OnScoreUpdate;
                    break;

                case ScoreSystem.ScoreType.CurrentScore:
                    m_ScoreSystem.CurrentScoreUpdateEvent -= OnScoreUpdate;
                    break;

                case ScoreSystem.ScoreType.Multiplier:
                    m_ScoreSystem.MultiplierUpdateEvent -= OnScoreUpdate;
                    break;

                default:
                    m_ScoreSystem.TotalScoreUpdateEvent -= OnScoreUpdate;
                    break;
            }
        }

        private void OnScoreUpdate(int score)
        {
            m_TargetScore = score;
        }

        private void Update()
        {
            if (m_TargetScore > m_DisplayedScore)
            {
                int diff = m_TargetScore - m_DisplayedScore;
                if (diff > 10)
                    diff = 10;

                m_DisplayedScore += diff;
                m_Text.text = m_DisplayedScore.ToString("000000");
            }
        }
    }
}