using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class MoveToDirection : MoveableObject
    {
        [SerializeField]
        private float m_Speed;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        [SerializeField]
        private Vector2 m_Direction;
        public Vector2 Direction
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }

        [SerializeField]
        private bool m_DoOffscreenCheck = false;

        public override void Move()
        {
            Vector3 velocity = new Vector3(m_Direction.x * m_Speed, m_Direction.y * m_Speed, 0.0f);
            Vector3 deltaMovement = velocity * Time.deltaTime;

            if (m_DoOffscreenCheck)
            {
                Vector3 newPos = transform.position + deltaMovement;
                if (!IsXOnScreen(newPos))
                    deltaMovement.x = 0.0f;

                if (!IsYOnScreen(newPos))
                    deltaMovement.y = 0.0f;
            }

            transform.Translate(deltaMovement, Space.World);
        }

        //FIX ME: SUPER DUPER LAME, but I wasn't in the mood
        private bool IsXOnScreen(Vector3 pos)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
            return !(viewPos.x < (0.0f) ||
                     viewPos.x > (1.0f));
        }

        private bool IsYOnScreen(Vector3 pos)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
            return !(viewPos.y < (0.0f) ||
                     viewPos.y > (1.0f));
        }
    }
}