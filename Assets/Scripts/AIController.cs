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
        private Pool m_ParticlePool;

        [SerializeField]
        private List<Gun> m_Guns;

        private void Awake()
        {
            if (m_MoveableObject == null)
                Debug.LogError("AI " + gameObject.name + " does not have a associated moveable object!");
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
            if (m_ParticlePool)
                m_ParticlePool.ActivateAvailableObject(m_DamageableObject.transform.position, m_DamageableObject.transform.rotation);

            gameObject.SetActive(false);
        }
    }
}