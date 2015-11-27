using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Schmup
{
    public enum MovePattern
    {
        Linear,
        Sinus,
        Cosinus
    }

    //Structs don't serialize well in Unity
    [System.Serializable]
    public class BulletSpawnDefinition
    {
        [SerializeField]
        private Texture2D m_Texture;
        public Texture2D Texture
        {
            get { return m_Texture; }
        }

        [SerializeField]
        private float m_Speed;
        public float Speed
        {
            get { return m_Speed; }
        }

        [SerializeField]
        private int m_Damage;
        public int Damage
        {
            get { return m_Damage; }
        }

        [SerializeField]
        private float m_ReloadTime;
        public float ReloadTime
        {
            get { return m_ReloadTime; }
        }

        [SerializeField]
        private MovePattern m_MovePattern;
        public MovePattern MovePattern
        {
            get { return m_MovePattern; }
        }

        [SerializeField]
        private float m_Frequency; //Unique to some movepatterns
        public float Frequency
        {
            get { return m_Frequency; }
        }

        [SerializeField]
        private float m_Amplitude; //Unique to some movepatterns
        public float Amplitude
        {
            get { return m_Amplitude; }
        }

        [SerializeField]
        private float m_Angle;
        public float Angle
        {
            get { return m_Angle; }
        }
    }

    public class Pattern : ScriptableObject
    {
        [SerializeField]
        private List<BulletSpawnDefinition> m_BulletSpawnDefinitions;
        public List<BulletSpawnDefinition> BulletSpawnDefinitions
        {
            get { return m_BulletSpawnDefinitions; }
        }

        public bool IsPatternValid()
        {
            //If not a single bullet has a reload time, we'll get an infinite loop
            foreach (BulletSpawnDefinition def in m_BulletSpawnDefinitions)
            {
                if (def.ReloadTime > 0.0f)
                    return true;
            }

            Debug.LogError("Pattern " + name + " doesn't have any bullet with a reload time! This will cause an infinite loop");
            return false;
        }
    }
}