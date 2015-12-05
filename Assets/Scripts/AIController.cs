using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private MoveableObject m_MoveableObject;

        [SerializeField]
        private DamageableObject m_DamageableObject;

        [SerializeField]
        private Pool m_DeathEffectPool;

        [SerializeField]
        private List<Gun> m_Guns;

        private void Awake()
        {
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
            if (m_MoveableObject == null)
                return;

            float horizInput = 0.0f;
            float vertInput = -1.0f;

            m_MoveableObject.Move(horizInput, vertInput);
        }

        private void HandleShooting()
        {
            if (m_Guns == null)
                return;

            if (m_Guns.Count == 0)
                return;

            //Fire all our guns
            foreach (Gun gun in m_Guns)
            {
                gun.Fire();
            }
        }

        private void OnDamage()
        {
        }

        private void OnDeath()
        {
            if (m_DeathEffectPool)
                m_DeathEffectPool.ActivateAvailableObject(m_DamageableObject.transform.position, m_DamageableObject.transform.rotation);

            gameObject.SetActive(false);
        }
    }
}