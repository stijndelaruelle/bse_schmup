using UnityEngine;
using System.Collections;

namespace Schmup
{
    public interface IDamageDealer
    {
        int GetDamage();
        void HadContact(GameObject go);
    }
}