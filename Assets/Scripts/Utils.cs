using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static  class Utils
    {
        public static void DisableRagdoll(GameObject gameObject)
        {
            foreach (var rb in gameObject.GetComponentsInChildren<Rigidbody>().Skip(1))
            {
                rb.isKinematic = true;
            }
        }

        public static void EnableRagdoll(GameObject gameObject)
        {
            foreach (var rb in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
        }
    }
}
