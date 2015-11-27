using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        private float m_Speed;

        [SerializeField]
        private List<Gun> m_Guns;

        public void Move(float dirX, float dirY)
        {
            Vector3 velocity = new Vector3(dirX * m_Speed, dirY * m_Speed, 0.0f);
            Vector3 deltaMovement = velocity * Time.deltaTime;

            transform.Translate(deltaMovement, Space.World);
        }

        public void Fire()
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

        //Collisions
    }
}