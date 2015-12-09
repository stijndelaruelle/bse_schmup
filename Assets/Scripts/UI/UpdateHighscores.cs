using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class UpdateHighscores : MonoBehaviour
    {
        [SerializeField]
        private HighscoreSystem m_HighscoreSystem;

        private void OnEnable()
        {
            m_HighscoreSystem.UpdateHighscores();
        }
    }
}
