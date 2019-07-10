using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EyeTracker.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EyeTracker
{
    public class EyeTracker : MonoBehaviour
    {
        // public int targetRadius;
        
        [HideInInspector]
        public MouseTracking mouseTracking;
        
        [HideInInspector]
        public MouseOutput mouseOutput = new MouseOutput();

//        public Vector4[] positions;
//        public float[] radiuses;
//        public float[] intensities;
        //Vector4[] properties;

//        public Material material;
//
//        public int count = 50;
    
        private Vector3 _mouse;
        
        [SerializeField]
        private GroupedData groupedData = new GroupedData();

        void Awake()
        {
           // material.SetFloatArray("_Points", new float[10]);
        }

        void Start()
        {
//            positions = new Vector4[count];
//            radiuses = new float[count];
//            intensities= new float[count];
//            properties = new Vector4[count];
//
//            for (int i = 0; i < positions.Length; i++)
//            {
//                positions[i] = new Vector4(Random.Range(-0.4f, +0.4f), Random.Range(-0.4f, +0.4f));
//                radiuses[i] = Random.Range(0f, 0.25f);
//                intensities[i] = Random.Range(-0.25f, 1f);
//            }
        }

        void Update()
        {
//            material.SetInt("_Points_Length", positions.Length);
//            for (int i = 0; i < positions.Length; i++)
//            {
//                positions[i] += new Vector4(Random.Range(-0.1f,+0.1f), Random.Range(-0.1f, +0.1f),0) * Time.deltaTime ;
//
//                properties[i] = new Vector4(radiuses[i], intensities[i],0,0);
//            }
//
//            material.SetVectorArray("_Points", positions);
//            material.SetVectorArray("_Properties", properties);
        }

        private void OnMouseOver()
        {
            AddTrackingData();
        }

        private void OnApplicationQuit()
        {
            SanitizeDataAndGenerateJson();
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
                    x = mouseOutputData.Key.XCoord,
                    y = mouseOutputData.Key.YCoord,
                    value = mouseOutputData.Count()
                }).ToList();

             return group;
        }

        private void SanitizeDataAndGenerateJson()
        {
            var aggregateTrackingData = AggregateTrackingData();

            foreach (var data in aggregateTrackingData)
            {
                groupedData.Group.Add(data);
            }

            CreateJsonFile(groupedData);
        }

        private void CreateJsonFile(GroupedData groupedData)
        {
            string json = JsonUtility.ToJson(groupedData);
            File.WriteAllText(@"Assets/Data/heatmap.json", json);
            
            string js = json.Remove(0, 9).TrimEnd('}');
            js = "const data = " + js + ";";
            File.WriteAllText(@"Assets/Data/heatmap.data.js", js);
        }
    }
}