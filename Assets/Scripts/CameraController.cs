using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private MoveableObject m_MoveableObject;

	    private void Update()
        {
            m_MoveableObject.Move();
	    }
    }
}