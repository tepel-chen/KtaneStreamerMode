using System;
using UnityEngine;

namespace StreamerService
{
    public class StreamerMode : MonoBehaviour
    {
        void Awake()
        {
            Patcher.Patch();
            Patcher.isActive = gameObject.activeSelf;
        }

        void OnEnable()
        {
            Patcher.isActive = true;
        }

        void OnDisable()
        {
            Patcher.isActive = false;
        }
    }
}
