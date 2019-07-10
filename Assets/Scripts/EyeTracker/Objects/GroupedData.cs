using System;
using System.Collections.Generic;
using UnityEngine;

namespace EyeTracker.Objects
{
    [Serializable]
    public class GroupedData
    {
        [SerializeField]
        public List<MouseOutputData> Group;
    }
}