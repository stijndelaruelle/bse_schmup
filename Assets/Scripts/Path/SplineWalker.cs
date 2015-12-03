using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    [SerializeField]
    private BezierSpline m_Spline;

    [SerializeField]
    private float m_Duration;

    private float m_Progress;

    private void Update()
    {
        m_Progress += Time.deltaTime / m_Duration;
        if (m_Progress > 1f)
        {
            m_Progress = 1f;
        }

        transform.localPosition = m_Spline.GetPoint(m_Progress);
    }
}