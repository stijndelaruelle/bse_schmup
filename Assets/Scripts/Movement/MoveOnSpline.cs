using UnityEngine;

namespace Schmup
{
    public class MoveOnSpline : MoveableObject
    {
        [SerializeField]
        private Spline m_Spline;

        [SerializeField]
        private float m_Speed;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        private float m_Duration;
        private float m_Progress;

        private void Start()
        {
            if (m_Spline == null)
            {
                Debug.LogWarning("SplineWalker doesn't have a spline!");
            }

            m_Duration = m_Speed * m_Spline.GetTotalLength();
        }

        public override void Move()
        {
            m_Progress += Time.deltaTime / m_Duration;
            if (m_Progress > 1f)
            {
                if (m_Spline.GetLoop() == true)
                {
                    m_Progress = 0.0f;
                }
                else
                {
                    m_Progress = 1f;
                }
            }

            transform.position = m_Spline.GetPoint(m_Progress);
        }
    }
}