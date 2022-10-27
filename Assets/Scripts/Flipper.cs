using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Flipper : MonoBehaviour
{
    public float flipStrength;

    Rigidbody rigidBody;
    Vector3 torque;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = Vector3.zero;
        Push(false);
    }

    public void Push(bool isPushed)
    { 
        torque = transform.forward * flipStrength * (isPushed ? -1 : 1);
    }

    private void FixedUpdate()
    {
        rigidBody.AddTorque(torque);
    }
}
