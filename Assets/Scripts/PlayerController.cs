using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Schmup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private int m_PlayerID;

        [SerializeField]
        private MoveableObject m_MoveableObject;

        [SerializeField]
        private DamageableObject m_DamageableObject;

        [SerializeField]
        private Pool m_DeathEffectPool;

        [SerializeField]
        private List<Gun> m_Guns;

        private event Action m_DeathEvent;
        public Action DeathEvent
        {
            get { return m_DeathEvent; }
            set { m_DeathEvent = value; }
        }


        private void Awake()
        {
            if (m_MoveableObject == null)
                Debug.LogError("Player " + m_PlayerID + " does not have a associated moveable object!");
        }

        private void Start()
        {
            m_DamageableObject.DamageEvent += OnDamage;
            m_DamageableObject.DeathEvent += OnDeath;
        }

        private void Update()
        {
            if (m_MoveableObject == null)
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

            m_MoveableObject.Move(horizInput, vertInput);
        }

        private void HandleShooting()
        {
            if (m_Guns == null)
                return;

            if (m_Guns.Count == 0)
                return;

            //We don't care about reloadtimes, those are for the gun
            bool fireInput = Input.GetButton("Fire_Player" + m_PlayerID);

            if (fireInput)
            {
                //Fire all our guns
                foreach (Gun gun in m_Guns)
                {
                    gun.Fire();
                }
            }
        }

        private void OnDamage()
        {
            //Debug.Log("The player took damage!");
        }

        private void OnDeath()
        {
            if (m_DeathEvent != null)
                m_DeathEvent();

            if (m_DeathEffectPool)
                m_DeathEffectPool.ActivateAvailableObject(m_DamageableObject.transform.position, m_DamageableObject.transform.rotation);

            gameObject.SetActive(false);
        }
    }
}