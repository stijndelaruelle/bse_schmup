using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class AIMoveController : MonoBehaviour
    {
        [SerializeField]
        private MoveableObject m_MoveableObject;

        private void Awake()
        {
            if (m_MoveableObject == null)
                Debug.LogError("AIContoller doesn't have a moveable object!");
        }

        private void Update()
        {
            if (m_MoveableObject == null)
                return;

            HandleMovement();
        }

        private void HandleMovement()
        {
            //Follow a path
            float horizInput = 0.0f;
            float vertInput = 0.0f;

            m_MoveableObject.Move(horizInput, vertInput);
        }
    }
}