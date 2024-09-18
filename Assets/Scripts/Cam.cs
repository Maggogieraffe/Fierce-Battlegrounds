using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class Cam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineComponentBase componentBase;
    float zoomedDistance;

    void Start()
    {
    }

    // Update is called once per frame  
    //void Update()
    //{
    //    Vector2 bal = Input.mouseScrollDelta;
    //    zoomedDistance += bal.y;
    //    zoomedDistance = Mathf.Clamp(zoomedDistance, -10, -1);
    //}
}
