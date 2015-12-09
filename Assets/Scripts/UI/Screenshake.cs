using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class Screenshake : MonoBehaviour
    {
        private Vector3 m_DefaultPosition;
        private bool m_Allow;

        private void Start()
        {
            m_DefaultPosition = transform.position.Copy();

            GlobalGameManager.Instance.GameStartEvent += OnGameStart;
            GlobalGameManager.Instance.GameCompleteEvent += OnGameComplete;
            GlobalGameManager.Instance.GameOverEvent += OnGameOver;
            GlobalGameManager.Instance.GameResetEvent += OnGameReset;
        }

        private void OnDestroy()
        {
            if (GlobalGameManager.Instance == null)
                return;

            GlobalGameManager.Instance.GameStartEvent -= OnGameStart;
            GlobalGameManager.Instance.GameCompleteEvent -= OnGameComplete;
            GlobalGameManager.Instance.GameOverEvent -= OnGameOver;
            GlobalGameManager.Instance.GameResetEvent -= OnGameReset;
        }

        public void StartShake(float strength, float length)
        {
            if (!m_Allow)
                return;

            StopShake();
            StartCoroutine(ScreenshakeRoutine(strength, length));
        }

        public void StopShake()
        {
            StopCoroutine("ScreenshakeRoutine");
            transform.position = m_DefaultPosition;
        }

        private IEnumerator ScreenshakeRoutine(float initialStrength, float length)
        {
            float timer = length;
            float strength = initialStrength;

            while (timer > 0 && m_Allow)
            {
                timer -= Time.deltaTime;
                strength = Mathf.Lerp(0.0f, initialStrength, timer);

                float shakeX = Random.Range(-strength, strength);
                float shakeY = Random.Range(-strength, strength);

                transform.position = new Vector3(m_DefaultPosition.x + shakeX,
                                                 m_DefaultPosition.y + shakeY,
                                                 m_DefaultPosition.z);

                yield return new WaitForEndOfFrame();
            }

            transform.position = m_DefaultPosition;
        }

        private void OnGameStart()
        {
            StopShake();
            m_Allow = true;
        }

        private void OnGameComplete()
        {
            StopShake();
            m_Allow = false;
        }

        private void OnGameOver()
        {
            StopShake();
            m_Allow = false;
        }

        private void OnGameReset()
        {
            StopShake();
            m_Allow = false;
        }
    }
}
