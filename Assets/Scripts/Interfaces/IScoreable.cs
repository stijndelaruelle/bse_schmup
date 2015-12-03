using UnityEngine;
using System.Collections;
using System;

namespace Schmup
{
    public interface IScoreable
    {
        Action<int> ScoreEvent { get; set; }
    }
}