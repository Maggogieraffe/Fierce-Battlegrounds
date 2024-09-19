using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Mouse")]
    public float MouseX;
    public float MouseY;
    [SerializeField] private float _MouseSensitivity = 5;

    [Header("Input")]
    public float horizontalInput;
    public float verticalInput;
    private float _movementSpeed;
    private Rigidbody _rB;
    private float runningMultiplier;

    [Header("GroundCheck")]
    public LayerMask whatIsGround;
    private bool _grounded;

        


    void Start()
    {
        _rB = GetComponent<Rigidbody>();
        _movementSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();

        MouseX += Input.GetAxis("Mouse X") * _MouseSensitivity;
        MouseY -= Input.GetAxis("Mouse Y") * _MouseSensitivity;

        //Rotating Camera around Player
        transform.rotation = Quaternion.Euler(0, MouseX, 0);
    }

    private void Moving()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if (verticalInput == 1)
        {
            runningMultiplier = 1.5f;
        }
        else
        {
            runningMultiplier = 1f;
        }

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);

        move = move.normalized * Time.deltaTime * _movementSpeed * runningMultiplier;
        transform.Translate(move);

    }
    void OnJump()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, whatIsGround);
        if (_grounded)
        {
            _rB.AddForce(Vector3.up * 13, ForceMode.Impulse);
        }
    }
}
