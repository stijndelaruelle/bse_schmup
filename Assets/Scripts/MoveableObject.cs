using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class MoveableObject : MonoBehaviour
    {
        [SerializeField]
        private float m_Speed;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        public void Move(float dirX, float dirY)
        {
            Vector3 velocity = new Vector3(dirX * m_Speed, dirY * m_Speed, 0.0f);
            Vector3 deltaMovement = velocity * Time.deltaTime;

            transform.Translate(deltaMovement, Space.World);
        }
    }
}