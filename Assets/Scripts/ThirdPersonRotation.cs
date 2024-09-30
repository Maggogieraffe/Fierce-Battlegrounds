using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonRotation : MonoBehaviour
{
    public float MouseX;
    public float MouseY;
    public float zoomedDistance;
    public float savedZoomedDistance;

    [SerializeField] private LayerMask Object;
    [SerializeField] private Transform _Follow;
    [SerializeField] private float _MouseSensitivity = 5;
    [SerializeField] private float _Sensitivity = 2;

    private bool Detected = false;
    private Camera _cam;
    private RaycastHit hit;

    private void Start()
    {
        zoomedDistance = 3;
        _cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        MovingMouse();
        CameraZooming();
        WallDetection();
    }
    private void MovingMouse()
    {
        MouseX += Input.GetAxis("Mouse X") * _MouseSensitivity;
        MouseY -= Input.GetAxis("Mouse Y") * _MouseSensitivity;
        MouseY = Mathf.Clamp(MouseY, -80, 80);
        
        //Rotating Camera around Player
        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);

    }
    private void LateUpdate()
    {
        transform.position = _Follow.position;
    }
    private void CameraZooming()
    {
        Vector2 bal = Input.mouseScrollDelta;
        zoomedDistance += bal.y;
        zoomedDistance = Mathf.Clamp(zoomedDistance, -20, -1);

        _cam.transform.localPosition = new Vector3(0, 0, zoomedDistance);
    }
    private void WallDetection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, -zoomedDistance / 3.2f, Object))
        {
            zoomedDistance = Mathf.Clamp(zoomedDistance, -hit.distance * 3.2f, -1);
        }
    }
}
