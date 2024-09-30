using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FirstMoveset : MonoBehaviour
{
    [SerializeField] private BoxCollider b_M1;
    public int o_M1;
    private bool M1stun = false;
    private bool stun = false;
    private Movement target;
    [SerializeField] private bool _isDummy;

    void Start()
    {

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
    }
    void Mouse1()
    {
        if (!M1stun)
        {
            if (o_M1 == 0)
            {
                M1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);
            }
            if (o_M1 == 1)
            {
                M1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);

            }
            if (o_M1 == 2)
            {
                M1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);

            }
            if (o_M1 == 3)
            {
                M1stun = true;
                Invoke("M1Stun", 0.5f);
                b_M1.enabled = true;
                Invoke("Attack", 0.1f);
                Invoke("LastM1Stun", 1.5f);
            }
            o_M1++;
        }
    }
    void Attack()
    {
        b_M1.enabled = false;
    }
    void M1Stun()
    {
        M1stun = false;
    }
    void LastM1Stun()
    {
        o_M1 = 0;
    }
}
