using UnityEngine;
using System.Collections;

namespace Schmup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private int m_PlayerID;

        [SerializeField]
        private Ship m_Ship;

        private void Awake()
        {
            if (m_Ship == null)
                Debug.LogError("Player " + m_PlayerID + " does not have a associated ship!");
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
            //Horizontal input
            float horizInput = Input.GetAxis("Horizontal_Player" + m_PlayerID);
            float horizInputKeyboard = Input.GetAxisRaw("HorizontalKey_Player" + m_PlayerID);

            if (horizInput == 0.0f)
                horizInput = horizInputKeyboard;

            //Vertical input
            float vertInput = Input.GetAxis("Vertical_Player" + m_PlayerID);
            float vertInputKeyboard = Input.GetAxisRaw("VerticalKey_Player" + m_PlayerID);

            if (vertInput == 0.0f)
                vertInput = vertInputKeyboard;

            m_Ship.Move(horizInput, vertInput);
        }

        private void HandleShooting()
        {
            //We don't care about reloadtimes, those are for the gun
            bool fireInput = Input.GetButton("Fire_Player" + m_PlayerID);

            if (fireInput)
                m_Ship.Fire();
        }
    }
}