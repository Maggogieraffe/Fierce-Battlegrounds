using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1Collider : MonoBehaviour
{
    private bool _hascollided = false;
    private FirstMoveset _moveset;
    private Movement _movement;
    private List<Rigidbody> _bodies = new List<Rigidbody>();
    private void Start()
    {
        _moveset = GetComponentInParent<FirstMoveset>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print(other.name);
            foreach (Rigidbody r in other.GetComponentsInChildren<Rigidbody>())
            {
                _bodies.Add(r);
            }
            _movement = other.GetComponent<Movement>();
            if (!_hascollided && _moveset.o_M1 < 3)
            {
                _hascollided = true;
                Invoke("CollidedOn", 0.2f);
                _movement.OnHit(4);
            }

            else if (!_hascollided && _moveset.o_M1 == 3)
            {
                print("Omae Wa...");
                _hascollided = true;
                Invoke("CollidedOn", 0.2f);
                _movement.Ragdolled();
                _movement.OnHit(4);
                _moveset._hitTarget = true;
                foreach (Rigidbody r in _bodies)
                {
                    r.AddForce(transform.forward * 800);
                }
            }
            _bodies.Clear();
        }
    }
    void CollidedOn()
    {
        _hascollided = false;
    }
}
        