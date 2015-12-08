using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class BossAIController : MonoBehaviour
    {
        [SerializeField]
        private RotateAround m_RotateObject;

        [SerializeField]
        private List<Gun> m_Phase1Guns;

        [SerializeField]
        private List<Gun> m_Phase2Guns;
        private List<Gun> m_Guns;

        [SerializeField]
        private DamageableObject m_DamageableObject;

        [SerializeField]
        private Pool m_DeathEffectPool;
        private bool m_ChangingPhase = false;
        private bool m_ChangedPhase = false;

        private void Start()
        {
            m_Guns = m_Phase1Guns;

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
            m_RotateObject.Move();
        }

        private void HandleShooting()
        {
            if (m_Guns == null)
                return;

            if (m_Guns.Count == 0)
                return;

            if (m_ChangingPhase)
                return;

            //Fire all our guns
            foreach (Gun gun in m_Guns)
            {
                gun.Fire();
            }
        }

        private void OnDamage()
        {
            //If we get below half our health, go to phase 2
            if (!m_ChangedPhase && !m_ChangingPhase && m_DamageableObject.Health < (m_DamageableObject.MaxHealth * 0.5f))
            {
                StartCoroutine(EnterPhase2Routine());
            }
        }

        private IEnumerator EnterPhase2Routine()
        {
            m_ChangingPhase = true;

            float m_OrigRotationSpeed = m_RotateObject.RotationSpeed;
            m_RotateObject.RotationSpeed = 5.0f;
            yield return new WaitForSeconds(3.0f);

            m_ChangingPhase = false;

            m_ChangedPhase = true;
            m_RotateObject.RotationSpeed = m_OrigRotationSpeed * 2.0f;
            m_Guns = m_Phase2Guns;
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