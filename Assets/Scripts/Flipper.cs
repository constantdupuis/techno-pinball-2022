using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(HingeJoint))]
public class Flipper : MonoBehaviour
{
    public float flipStrength = 10;

    //private Rigidbody rigidBody;
    private HingeJoint hinge;

    private JointMotor motor;

    //private Vector3 torque;

    private void Awake()
    {
        //rigidBody = GetComponent<Rigidbody>();
        //rigidBody.centerOfMass = Vector3.zero;

        hinge = GetComponent<HingeJoint>(); 
        hinge.useMotor = true;
        //motor = new JointMotor();
        motor = hinge.motor;
        motor.force = flipStrength;
        //motor.freeSpin = false;
        //motor.targetVelocity = 180;

        //Push(true);
    }

    public void Push(InputAction.CallbackContext context)
    {
        Debug.Log($"Started : {context.started}, Performed : {context.performed}, Canceled : {context.canceled},");
        //torque = transform.forward * flipStrength * (isPushed ? -1 : 1);
        //if (context.performed)
        //{
        //    motor.targetVelocity = 360;
        //}
        //else {
        //    motor.targetVelocity = -360;
        //}
        motor.targetVelocity = (context.performed ? 1 : -1) * 360;
        //motor.force = flipStrength * (context.performed ? -1 : 1);
        hinge.motor = motor;
    }
}