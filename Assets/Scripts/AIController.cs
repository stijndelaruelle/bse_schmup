using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private Ship m_Ship;

        private void Awake()
        {
            if (m_Ship == null)
                Debug.LogError("AIContoller doesn't have a ship!");
        }

        private void Update()
        {
            if (m_Ship == null)
                return;

            HandleMovement();
            HandleShooting();
        }

        private void HandleMovement()
        {
            //Follow a path
            float horizInput = 0.0f;
            float vertInput = 0.0f;

            m_Ship.Move(horizInput, vertInput);
        }

        private void HandleShooting()
        {
            //The AI Always fires
            m_Ship.Fire();
        }
    }
}