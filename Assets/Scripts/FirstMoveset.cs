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
    private bool _m1resetbool;
    public bool _hitTarget = false;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _movementSpeed = _movement._movementSpeed;
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
            if (o_M1 == 0)
            {
                _m1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);
                _m1resetbool = true;
            }
            if (o_M1 == 1)
            {
                _m1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);
                _m1resettimer = 0;

            }
            if (o_M1 == 2)
            {
                _m1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);
                _m1resettimer = 0;
            }
            if (o_M1 == 3)
            {
                _m1stun = true;
                Invoke("M1Stun", 0.5f);
                Invoke("DidHit", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);
                Invoke("LastM1Stun", 1.5f);
                _m1resettimer = 0;
                _m1resetbool = false;
            }
            o_M1++;
        }
    }
    void M1Stun()
    {
        _m1stun = false;
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
    void Attack()
    {
        b_M1.enabled = false;
    }
    void LastM1Stun()
    {
        o_M1 = 0;
    }
}
