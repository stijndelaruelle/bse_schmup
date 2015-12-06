using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class TimedPoolableObject : PoolableObject
    {
        [SerializeField]
        private float m_DisableTime = 1.0f;
        private float m_Timer = 0.0f;
        private Transform m_Parent = null;

        private void Start()
        {
            GlobalGameManager.Instance.GameResetEvent += OnGameReset;
        }

        private void OnDestroy()
        {
            if (GlobalGameManager.Instance != null)
                GlobalGameManager.Instance.GameResetEvent -= OnGameReset;
        }

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_DisableTime)
                Deactivate();
        }

        #region PoolableObject

        public override void Initialize()
        {
            m_Parent = transform.parent;
        }

        public override void Activate(Vector3 pos, Quaternion rot)
        {
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rot;

            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            transform.parent = m_Parent;
            gameObject.SetActive(false);
            m_Timer = 0.0f;
        }

        public override bool IsAvailable()
        {
            return (!gameObject.activeSelf);
        }

        #endregion

        private void OnGameReset()
        {
            Deactivate();
        }
    }
}
