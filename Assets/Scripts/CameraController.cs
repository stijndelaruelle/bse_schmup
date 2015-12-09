using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private MoveableObject m_MoveableObject;

        [SerializeField]
        private float m_PauseY;

	    private void Update()
        {
            if (transform.position.y > m_PauseY)
                return;

            m_MoveableObject.Move();
	    }
    }
}