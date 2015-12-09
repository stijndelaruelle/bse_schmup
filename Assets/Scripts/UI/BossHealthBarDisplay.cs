using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Schmup
{
    public class BossHealthBarDisplay : MonoBehaviour
    {
        [SerializeField]
        private BossAIController m_Boss;
        private DamageableObject m_DamageableObject;

        [SerializeField]
        private RectTransform m_HealthBar;
        private Vector2 m_OriginalSize;

        private void Start()
        {
            if (m_Boss == null)
            {
                Debug.LogError("BossHealthBarDisplay doesn't have a boss reference!");
                return;
            }

            m_HealthBar.gameObject.SetActive(false);
            m_DamageableObject = m_Boss.DamageableObject;

            m_OriginalSize = m_HealthBar.sizeDelta.Copy();

            m_DamageableObject.HealEvent += OnHeal;
            m_DamageableObject.DamageEvent += OnDamage;
            m_DamageableObject.DeathEvent += OnDeath;
            m_Boss.EnableEvent += OnBossEnable;
            m_Boss.DisableEvent += OnBossDisable;
        }

        private void OnDestroy()
        {
            if (m_Boss == null)
                return;

            m_DamageableObject.HealEvent -= OnHeal;
            m_DamageableObject.DamageEvent -= OnDamage;
            m_DamageableObject.DeathEvent -= OnDeath;
            m_Boss.EnableEvent -= OnBossEnable;
            m_Boss.DisableEvent -= OnBossDisable;
        }

        private void OnHeal()
        {
            UpdateHealth();
        }

        private void OnDamage()
        {
            UpdateHealth();
        }

        private void OnBossEnable()
        {
            m_HealthBar.gameObject.SetActive(true);
        }

        private void OnBossDisable()
        {
            m_HealthBar.gameObject.SetActive(false);
        }

        private void UpdateHealth()
        {
            int currentHealth = m_DamageableObject.Health;
            int maxHealth = m_DamageableObject.MaxHealth;

            float percent = (float)currentHealth / (float)maxHealth;

            m_HealthBar.sizeDelta = new Vector2(m_OriginalSize.x * percent, m_OriginalSize.y);
        }

        private void OnDeath()
        {
            m_HealthBar.gameObject.SetActive(false);
        }
    }
}