using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private List<MoveableObject> m_MoveableObjects;

        [SerializeField]
        private List<Gun> m_Guns;

        [SerializeField]
        private DamageableObject m_DamageableObject;

        [SerializeField]
        private Pool m_DeathEffectPool;

        private void Awake()
        {
        }

        private void Start()
        {
            m_DamageableObject.DamageEvent += OnDamage;
            m_DamageableObject.DeathEvent += OnDeath;
        }

        private void OnDestroy()
        {
            m_DamageableObject.DamageEvent -= OnDamage;
            m_DamageableObject.DeathEvent -= OnDeath;
        }

        private void Update()
        {
            if (!IsOnScreen())
                return;

            HandleMovement();
            HandleShooting();
        }

        private void HandleMovement()
        {
            if (m_MoveableObjects == null)
                return;

            if (m_MoveableObjects.Count == 0)
                return;

            //Fire all our guns
            foreach (MoveableObject moveableObject in m_MoveableObjects)
            {
                moveableObject.Move();
            }
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

        private bool IsOnScreen()
        {
            float offset = 0.1f;

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            return !(viewPos.x < (0.0f - offset) ||
                     viewPos.x > (1.0f + offset) ||
                     viewPos.y < (0.0f - offset) || 
                     viewPos.y > (1.0f + offset));
        }
    }
}