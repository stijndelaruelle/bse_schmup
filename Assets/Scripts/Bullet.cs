using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class Bullet : PoolableObject, IDamageDealer
    {
        [SerializeField]
        private SpriteRenderer m_SpriteRenderer;
        public Sprite Sprite
        {
            get { return m_SpriteRenderer.sprite; }
            set { m_SpriteRenderer.sprite = value; }
        }

        private float m_Speed;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        private int m_Damage;
        public int Damage
        {
            get { return m_Damage; }
            set { m_Damage = value; }
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
                Deactivate();
        }

        #region PoolableObject

        public override void Activate(Vector3 pos, Quaternion rot)
        {
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rot;

            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
            m_Timer = 0.0f;
        }

        public override bool IsAvailable()
        {
            return (!gameObject.activeSelf);
        }

        #endregion

        #region IDamageDealer

        public int GetDamage()
        {
            return m_Damage;
        }

        public void DealtDamage()
        {
            //Spawn particle effect etc.
            Deactivate();
        }

        #endregion

    }
}