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

        public override void Move()
        {
            Vector3 velocity = new Vector3(m_Direction.x * m_Speed, m_Direction.y * m_Speed, 0.0f);
            Vector3 deltaMovement = velocity * Time.deltaTime;

            transform.Translate(deltaMovement, Space.World);
        }
    }
}