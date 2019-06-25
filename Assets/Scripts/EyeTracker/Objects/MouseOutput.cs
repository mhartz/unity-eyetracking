using System;
using System.Collections.Generic;
using UnityEngine;

namespace EyeTracker.Objects
{
    [Serializable]
    public class MouseOutput
    {
        [SerializeField]
        public List<MouseTracking> Data;
    }
}