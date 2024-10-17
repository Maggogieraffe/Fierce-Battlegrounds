using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FirstMoveset : MonoBehaviour
{
    [SerializeField] private BoxCollider b_M1;
    public int o_M1;
    private bool _m1stun = false;
    private bool stun = false;
    private Movement target;
    [SerializeField] private bool _isDummy;
    private Movement _movement;
    private float _movementSpeed;
    private bool _bM1Stun = false;
    private float _m1resettimer;
    public bool _m1resetbool = false;
    public bool _hitTarget = false;
    private Animator _animator;

    void Start()    
    {
        _movement = GetComponent<Movement>();
        _movementSpeed = _movement._movementSpeed;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_isDummy)
        {
            if (Input.GetMouseButton(0))
            {
                Mouse1();
            }
        }
        if (!_bM1Stun && _m1stun)
        {
            _bM1Stun = true;
            _movement._movementSpeed = _movementSpeed / 2;
        }
        else if (_bM1Stun && !_m1stun)
        {
            _bM1Stun = false;
            _movement._movementSpeed = _movementSpeed;
        }
        if (_m1resetbool)
        {
            _m1resettimer += Time.deltaTime;
            if (_m1resettimer >= 1)
            {
                _m1resettimer = 0;
                o_M1 = 0;
                _m1resetbool = false;
            }
        }
    }
    void Mouse1()
    {
        if (!_m1stun)
        {
            // Trigger the corresponding attack animation
            switch (o_M1)
            {
                case 0:
                    _animator.SetTrigger("Attack1"); // First attack animation
                    _m1stun = true;
                    Invoke("M1Stun", .5f);
                    Invoke("EnableCollider", 0.2f);
                    Invoke("DisableCollider", 0.3f);
                    _m1resetbool = true;
                    break;
                case 1:
                    _animator.SetTrigger("Attack2"); // Second attack animation
                    _m1stun = true;
                    Invoke("M1Stun", .5f);
                    Invoke("EnableCollider", 0.2f);
                    Invoke("DisableCollider", 0.3f);
                    _m1resettimer = 0;
                    break;
                case 2:
                    _animator.SetTrigger("Attack3"); // Third attack animation
                    _m1stun = true;
                    Invoke("M1Stun", .5f);
                    Invoke("EnableCollider", 0.2f);
                    Invoke("DisableCollider", 0.3f);
                    _m1resettimer = 0;
                    break;
                case 3:
                    _animator.SetTrigger("Attack4"); // Fourth attack animation (finisher)
                    _m1stun = true;
                    Invoke("M1Stun", .5f);
                    Invoke("DidHit", .5f);
                    Invoke("EnableCollider", 0.2f);
                    Invoke("DisableCollider", 0.3f);
                    Invoke("LastM1Stun", 1.5f);
                    _m1resettimer = 0;
                    break;
            }
        }
    }
    void M1Stun()
    {
        _m1stun = false;
        o_M1++;
    }
    void DidHit()
    {
        if (!_hitTarget)
        {
            _movement._movementSpeed = 0;
        }
        Invoke("OutHit", 1f);
        _m1stun = true;
    }
    void OutHit()
    {
        _movement._movementSpeed = _movementSpeed;
        _m1stun = false;
        _hitTarget = false;
    }
    void EnableCollider()
    {
        b_M1.enabled = true;
    }
    void DisableCollider()
    {
        b_M1.enabled = false;
    }
    void LastM1Stun()
    {
        o_M1 = 0;
        _m1resetbool = false;
    }
}
