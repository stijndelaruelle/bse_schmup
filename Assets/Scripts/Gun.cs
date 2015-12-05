using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private Pool m_BulletPool;

        [SerializeField]
        private bool m_RotateTowardsPlayer;

        [SerializeField]
        private Pattern m_Pattern;

        [SerializeField]
        private AudioSource m_FireSound;

        private int m_CurrentBulletID;
        private bool m_IsReloading;

        private void Start()
        {
            //If the pattern isn't valid, don't use it or we'll get into trouble.
            if (m_Pattern != null && !m_Pattern.IsPatternValid())
                m_Pattern = null;
        }

        public void Fire()
        {
            //Don't shoot if we're off screen
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.x < 0.0f || viewPos.x > 1.0f || viewPos.y < 0.0f || viewPos.y > 1.0f)
                return;

            if (m_Pattern == null || m_IsReloading)
                return;

            List<BulletSpawnDefinition> bulletSpawnDefinitions = m_Pattern.BulletSpawnDefinitions;

            while (!m_IsReloading)
            {
                for (int i = m_CurrentBulletID; i < bulletSpawnDefinitions.Count; ++i)
                {
                    m_CurrentBulletID = i + 1;

                    //Get bullet from the pool & spawn it
                    InitializeBullet(bulletSpawnDefinitions[i]);

                    //Reload if required
                    if (bulletSpawnDefinitions[i].ReloadTime > 0.0f)
                    {
                        Reload(bulletSpawnDefinitions[i].ReloadTime);
                        break;
                    }
                }

                //reset
                if (m_CurrentBulletID >= bulletSpawnDefinitions.Count)
                    m_CurrentBulletID = 0;
            }
        }

        private void InitializeBullet(BulletSpawnDefinition bulletDefinition)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion addedRotation = Quaternion.AngleAxis(-bulletDefinition.Angle, new Vector3(0.0f, 0.0f, 1.0f));
            Quaternion totalRotation = currentRotation * addedRotation;

            //FIX super dirty, try to use templates!
            PoolableObject obj = m_BulletPool.ActivateAvailableObject(transform.position, totalRotation);

            if (m_BulletPool.IsPoolType<Bullet>())
            {
                Bullet bullet = (Bullet)obj;
                bullet.Speed = bulletDefinition.Speed;
                bullet.Damage = bulletDefinition.Damage;
                bullet.MovePattern = bulletDefinition.MovePattern;
                bullet.Frequency = bulletDefinition.Frequency;
                bullet.Amplitude = bulletDefinition.Amplitude;

                if (bulletDefinition.Sprite != null)
                    bullet.Sprite = bulletDefinition.Sprite;

                //Set the tag
                bullet.gameObject.tag = gameObject.tag;
            }

            if (m_FireSound != null)
                m_FireSound.Play();
        }

        private void Reload(float reloadTime)
        {
            m_IsReloading = true;

            StopAllCoroutines();
            StartCoroutine(ReloadRoutine(reloadTime));
        }

        private IEnumerator ReloadRoutine(float reloadTime)
        {
            yield return new WaitForSeconds(reloadTime);
            m_IsReloading = false;
        }

        private void Update()
        {
            Vector3 forward = transform.TransformDirection(Vector3.up) * 10;
            Debug.DrawRay(transform.position, forward, Color.yellow);
        }
    }
}