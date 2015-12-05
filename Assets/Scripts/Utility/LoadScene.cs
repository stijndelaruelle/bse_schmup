using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private string m_SceneName;

        void Start()
        {
            Application.LoadLevel(m_SceneName);
        }

    }
}
