using System;
using System.IO;
using EyeTracker.Objects;
using UnityEngine;

namespace EyeTracker
{
    public class EyeTracker : MonoBehaviour
    {
        public int targetRadius;
        
        [HideInInspector]
        public MouseTracking mouseTracking;
        
        [HideInInspector]
        public MouseOutput mouseOutput = new MouseOutput();
    
        private Vector3 _mouse;

        private void OnMouseOver()
        {
            AddTrackingData();
        }

        private void OnApplicationQuit()
        {
            CreateJsonFile();
        }

        private void AddTrackingData()
        {
            _mouse = Input.mousePosition;

            mouseTracking = new MouseTracking
            {
                XCoord = (int)Math.Round(_mouse.x),
                YCoord = (int)Math.Round(_mouse.y)
            };

            mouseOutput.Data.Add(mouseTracking);
        }

        private void CreateJsonFile()
        {   
            string json = JsonUtility.ToJson(mouseOutput);
            File.WriteAllText(@"Assets/Data/heatmap.json", json);
            
            string js = json.Remove(0, 8).TrimEnd('}');
            js = "data = '" + js + "';";
            File.WriteAllText(@"Assets/Data/heatmap.data.js", js);
        }
    }
}