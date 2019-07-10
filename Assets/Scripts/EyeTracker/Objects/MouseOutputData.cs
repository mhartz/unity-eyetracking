using System;
using UnityEngine;

namespace EyeTracker.Objects
{
    [Serializable]
    public class MouseOutputData
    {
        [SerializeField] public int x;
        [SerializeField] public int y;
        [SerializeField] public int value;
    }
}