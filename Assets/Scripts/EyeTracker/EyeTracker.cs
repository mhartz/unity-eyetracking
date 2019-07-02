using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EyeTracker.Objects;
using UnityEngine;

namespace EyeTracker
{
    public class EyeTracker : MonoBehaviour
    {
        // public int targetRadius;
        
        [HideInInspector]
        public MouseTracking mouseTracking;
        
        [HideInInspector]
        public MouseOutput mouseOutput = new MouseOutput();

        public Vector4[] points;
        public Material heatmapMaterial;
    
        private Vector3 _mouse;

        void Update()
        {
            heatmapMaterial.SetInt("_PointsSize", points.Length);
            heatmapMaterial.SetVectorArray("_Points", points);
        }

        private void OnMouseOver()
        {
            AddTrackingData();
        }

        private void OnApplicationQuit()
        {
            var aggregateTrackingData = AggregateTrackingData(); 
            foreach (var data in aggregateTrackingData)
            {
                Debug.Log("/////////////////////////////////");
                Debug.Log(data.XCoord.ToString());
                Debug.Log(data.YCoord.ToString());
                Debug.Log(data.Fixation.ToString());
            }
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

        private IEnumerable<MouseOutputData> AggregateTrackingData()
        {
            var group = mouseOutput.Data
                .GroupBy(point => new
                {
                    point.XCoord,
                    point.YCoord
                })
                .Select(mouseOutputData => new MouseOutputData
                {
                    XCoord = mouseOutputData.Key.XCoord,
                    YCoord = mouseOutputData.Key.YCoord,
                    Fixation = mouseOutputData.Count()
                })
                .ToList();

             return group;
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