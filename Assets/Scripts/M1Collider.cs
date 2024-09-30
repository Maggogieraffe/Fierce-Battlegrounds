using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1Collider : MonoBehaviour
{
    private FirstMoveset _moveset;
    private List<Rigidbody> _bodies = new List<Rigidbody>();
    private void Start()
    {
        _moveset = GetComponentInParent<FirstMoveset>();
    }
    private void OnTriggerStay(Collider other)
    {
        foreach (Rigidbody r in other.GetComponentsInChildren<Rigidbody>())
        {
            _bodies.Add(r);
        }
        var movement = other.GetComponent<Movement>();
        if (_moveset.o_M1 < 3)
        {
            movement.OnHit(4);
        }
        else if (_moveset.o_M1 == 4)
        {
            movement.Ragdolled();
            movement.OnHit(4);
            foreach (Rigidbody r in _bodies)
            {
                r.AddForce(transform.forward * 100);
            }
            //other.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        }
        _bodies.Clear();

    }
}
        