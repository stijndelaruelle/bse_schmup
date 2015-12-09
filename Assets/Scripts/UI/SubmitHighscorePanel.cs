using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Schmup
{
    public class SubmitHighscorePanel : MonoBehaviour
    {
        [SerializeField]
        private ScoreSystem m_ScoreSystem;

        [SerializeField]
        private HighscoreSystem m_HighscoreSystem;

        [SerializeField]
        private InputField m_PlayerName;

        [SerializeField]
        private InputField m_Email;

        [SerializeField]
        private GameObject m_PanelToHide;

        [SerializeField]
        private GameObject m_PanelToShow;

        private void Start()
        {
            m_HighscoreSystem.HighscorePostedEvent += OnHighscorePosted;
        }

        private void OnDestroy()
        {
            if (m_HighscoreSystem != null)
                m_HighscoreSystem.HighscorePostedEvent -= OnHighscorePosted;
        }

        public void SubmitHighscore()
        {
            m_HighscoreSystem.PostHighscore(m_PlayerName.text, m_Email.text, m_ScoreSystem.TotalScore);
        }

        private void OnHighscorePosted()
        {
            m_PanelToHide.SetActive(false);
            m_PanelToShow.SetActive(true);
        }

        private void OnDisable()
        {
            m_PanelToHide.SetActive(true);
            m_PanelToShow.SetActive(false);
        }
    }
}
