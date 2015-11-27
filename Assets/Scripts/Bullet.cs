using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class Bullet : PoolableObject
    {
        private float m_Speed;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        private MovePattern m_MovePattern;
        public MovePattern MovePattern
        {
            get { return m_MovePattern; }
            set { m_MovePattern = value; }
        }

        private float m_Amplitude;
        public float Amplitude
        {
            get { return m_Amplitude; }
            set { m_Amplitude = value; }
        }

        private float m_Frequency;
        public float Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }

        private float m_Timer;

        private void Update()
        {
            HandleMovement();
            DisableIfOffScreen();
        }

        private void HandleMovement()
        {
            Vector3 velocity = new Vector3(transform.up.x * m_Speed, transform.up.y * m_Speed, 0.0f);
            Vector3 deltaMovement = velocity * Time.deltaTime;

            transform.Translate(deltaMovement, Space.World);

            switch (m_MovePattern)
            {
                case MovePattern.Sinus:
                    {
                        transform.position += m_Amplitude * Mathf.Sin(((Mathf.PI / 2) + m_Timer) * m_Frequency) * transform.right;
                    }
                    break;

                case MovePattern.Cosinus:
                    {
                        transform.position += m_Amplitude * Mathf.Cos(m_Timer * m_Frequency) * transform.right;
                    }
                    break;
            }

            m_Timer += Time.deltaTime;
        }

        private void DisableIfOffScreen()
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.x < 0.0f || viewPos.x > 1.0f || viewPos.y < 0.0f || viewPos.y > 1.0f)
                Reset();
        }

        #region PoolableObject

        public override void Activate(Vector3 pos, Quaternion rot)
        {
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rot;

            gameObject.SetActive(true);
        }

        public override void Reset()
        {
            gameObject.SetActive(false);
            m_Timer = 0.0f;
        }

        public override bool IsAvailable()
        {
            return (!gameObject.activeSelf);
        }

        #endregion
    }
}