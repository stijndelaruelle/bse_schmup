using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class AIGunController : MonoBehaviour
    {
        [SerializeField]
        private List<Gun> m_Guns;

        private void Awake()
        {
            if (m_Guns == null)
                Debug.LogError("AIGunController doesn't have a gun list!");

            if (m_Guns.Count == 0)
                Debug.LogError("AIGunController doesn't have any gun!");
        }

        private void Update()
        {
            if (m_Guns.Count == 0)
                return;

            HandleShooting();
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
    }
}