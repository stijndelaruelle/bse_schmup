﻿using UnityEngine;
using System.Collections;

namespace Schmup
{
    //Abstract class instead of interface as we have to 100% certain it's a monobehaviour.
    public abstract class PoolableObject : MonoBehaviour
    {
        public abstract void Activate(Vector3 pos, Quaternion rot);
        public abstract void Reset();
        public abstract bool IsAvailable();
    }
}