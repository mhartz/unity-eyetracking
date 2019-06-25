using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

[Serializable]
public class MouseTracking
{
    public float xCoord { get; set; }
    public float yCoord { get; set; }
}

[Serializable]
public class MouseOutput
{
    public object data;
}

public class EyeTracker : MonoBehaviour
{
    [SerializeField]
    public MouseTracking mouseTracking;
    
    [SerializeField]
    public MouseOutput mouseOutput;
    
    private Vector3 mouse;
    private string json;

    private void OnMouseOver()
    {
        mouse = Input.mousePosition;

        mouseTracking = new MouseTracking
        {
            xCoord = mouse.x,
            yCoord = mouse.y
        };

        mouseOutput.data = mouseTracking;
    }

    void OnApplicationQuit()
    {
        MemoryStream stream = new MemoryStream();
        
        json = JsonUtility.ToJson(mouseOutput.data);
        Debug.Log(json);
    }
}
