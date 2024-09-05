using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineComponentBase componentBase;
    float zoomedDistance;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        var componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        Vector2 bal = Input.mouseScrollDelta;
        zoomedDistance -= bal.y;
        zoomedDistance = Mathf.Clamp(zoomedDistance, 1, 10);
        if (componentBase is Cinemachine3rdPersonFollow)
        {
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = zoomedDistance;

        }
    }
}
