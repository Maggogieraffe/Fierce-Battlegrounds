using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;
public enum PlayerState
{
    Idle = 0,
    walking,
    running,
    ragdoll,
    dead
}
public class Movement : MonoBehaviour
{
    [Header("Mouse")]
    public float MouseX;
    public float MouseY;
    [SerializeField] private float _MouseSensitivity = 5;

    [Header("Input")]
    public float horizontalInput;
    public float verticalInput;

    [Header("GroundCheck")]
    public LayerMask whatIsGround;
    private bool _grounded;

    [Header("Values")]
    public float _movementSpeed;
    private float runningMultiplier;

    [Header("Combat")]
    public bool HitStunned = false;
    public bool IsRagdolled = false;

    [Header("Other")]
    [SerializeField] private Transform _torso;
    private Animator _animator;
    private PlayerState _playerState;
    private Rigidbody _rB;
    public List<Rigidbody> _rbodies;
    [SerializeField] private bool _isDummy;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rB = GetComponent<Rigidbody>();
        _movementSpeed = 6f;
        _playerState = PlayerState.Idle;
        _animator.SetInteger("State", (int)_playerState);

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDummy)
        {
            Moving();

            MouseX += Input.GetAxis("Mouse X") * _MouseSensitivity;
            MouseY -= Input.GetAxis("Mouse Y") * _MouseSensitivity;

            //Rotating Camera around Player
            transform.rotation = Quaternion.Euler(0, MouseX, 0);
        }
    }

    private void Moving()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput == 1)
        {
            runningMultiplier = 1.5f;
            _playerState = PlayerState.running;
        }
        else if (verticalInput == -1 || horizontalInput == 1 || horizontalInput == -1)
        {
            runningMultiplier = 1f;
            _playerState = PlayerState.walking;

        }
        else if (verticalInput == 0 && horizontalInput == 0)
        {
            _playerState = PlayerState.Idle;    
        }
        _animator.SetInteger("State", (int)_playerState);

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
    public void OnHit(float damage)
    {
        
    }
    public void Ragdolled()
    {
        _playerState = PlayerState.ragdoll;
        _animator.enabled = false;
        foreach (Rigidbody r in _rbodies)
        {
            r.isKinematic = false;
        }
        Invoke("UnRagdolled", 2f);
    }
    public void UnRagdolled()
    {
        transform.position = _torso.position;
        foreach (Rigidbody r in _rbodies)
        {
            r.isKinematic = false;
        }
        _animator.enabled = true;
    }
}
