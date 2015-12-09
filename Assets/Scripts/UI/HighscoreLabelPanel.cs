using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Schmup
{
    public class HighscoreLabelPanel : MonoBehaviour
    {
        [SerializeField]
        private HighscoreSystem m_HighscoreSystem;

        [SerializeField]
        private int m_HighscoreID;

        [SerializeField]
        private Text m_IDText;

        [SerializeField]
        private Text m_NameText;

        [SerializeField]
        private Text m_ScoreText;

        private void Start()
        {
            m_HighscoreSystem.HighscoreUpdatedEvent += OnHighscoreUpdated;
        }

        private void OnDestroy()
        {
            if (m_HighscoreSystem != null)
                m_HighscoreSystem.HighscoreUpdatedEvent -= OnHighscoreUpdated;
        }

        private void OnHighscoreUpdated()
        {
            Highscore highscore = m_HighscoreSystem.GetHighscore(m_HighscoreID);

            m_IDText.text = (m_HighscoreID + 1) + ".";
            m_NameText.text = highscore.name;
            m_ScoreText.text = highscore.score.ToString();
        }
    }
}
