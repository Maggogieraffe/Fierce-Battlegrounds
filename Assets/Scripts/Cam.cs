using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineComponentBase componentBase;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bal = Input.mouseScrollDelta;
        print(bal);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance += bal.y; //CameraZoomDistance
        }
    }
}
