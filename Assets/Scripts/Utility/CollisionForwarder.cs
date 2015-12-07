using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Schmup
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionForwarder : MonoBehaviour
    {
        [SerializeField]
        private List<string> m_TagFilter;

        private event Action<Collider2D> m_TriggerEnterEvent;
        public Action<Collider2D> TriggerEnterEvent
        {
            get { return m_TriggerEnterEvent; }
            set { m_TriggerEnterEvent = value; }
        }

        private event Action<Collider2D> m_TriggerExitEvent;
        public Action<Collider2D> TriggerExitEvent
        {
            get { return m_TriggerExitEvent; }
            set { m_TriggerExitEvent = value; }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_TagFilter.Count == 0 || m_TagFilter.Contains(other.tag))
            {
                if (m_TriggerEnterEvent != null)
                    m_TriggerEnterEvent(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_TagFilter.Count == 0 || m_TagFilter.Contains(other.tag))
            {
                if (m_TriggerExitEvent != null)
                    m_TriggerExitEvent(other);
            }
        }
    }
}