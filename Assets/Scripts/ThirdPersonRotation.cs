using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonRotation : MonoBehaviour
{
    public Transform PlayerMoving;
    public float MouseX;
    public float MouseY;
    public float Rotation;
    public float countingDelta = 0;
    public float MinCamZoom = 4;
    public float MaxCamZoom = 16;

    [SerializeField] private Transform _Cam;
    [SerializeField] private Transform _Player;
    [SerializeField] private float _MouseSensitivity = 5;
    [SerializeField] private float _Sensitivity = 2;

    private Camera _cam;
    private Vector2 _rotate;

    private float _distancePlayerToCam;
    private float _scrollWheel;
    private float _mouseX;
    private float _mouseY;
    private void Start()
    {
        _cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        MovingMouse();
        CameraZooming();
        //have the MarbleMoving rotation set to the freelook rotation
        if (Input.GetMouseButtonUp(1))
        {
            PlayerMoving.rotation = transform.rotation;
        }
    }
    private void MovingMouse()
    {
        MouseX += Input.GetAxis("Mouse X") * _MouseSensitivity;
        MouseY -= Input.GetAxis("Mouse Y") * _MouseSensitivity;

        //have the freelook gameobject follow the marble
        transform.position = _Player.position;
        //this is a variable for the tutorial UI
        Rotation = transform.rotation.y;
        //this gameObject is the direction the marble moves and this makes it follow the marble
        PlayerMoving.position = _Player.position;


        //Rotating Camera around Player
        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);

        //If you press RMB the MarbleMoving will stop rotating but the camera will still rotate,
        //so you can look around freely while the player is still rolling forward
        if (!Input.GetMouseButton(1))
        {
            PlayerMoving.rotation = transform.rotation;
        }
    }
    private void CameraZooming()
    {
        _scrollWheel = Input.GetAxis("Mouse ScrollWheel") * 1.5f;

        _distancePlayerToCam = Vector3.Distance(transform.position, _Cam.position);

        //_scrollWheel = Mathf.Clamp(_scrollWheel, 4, 16);
        _Cam.transform.Translate(Vector3.forward * _scrollWheel);
    }
}
