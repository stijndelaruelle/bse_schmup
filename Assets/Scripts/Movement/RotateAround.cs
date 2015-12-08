using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class RotateAround : MoveableObject
    {
        [SerializeField]
        private float m_RotationSpeed;
        public float RotationSpeed
        {
            get { return m_RotationSpeed; }
            set { m_RotationSpeed = value; }
        }

        public override void Move()
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), m_RotationSpeed * Time.deltaTime);
        }
    }
}