using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    /*Serialize*/

    //Car Settings
    [Header("Car Settings")] [SerializeField]
    private float motorTorque;

    [SerializeField] private float brakeForce;
    [Range(0, 180)] [SerializeField] private float steeringAngle;
    [Range(500, 4000)] [SerializeField] private int carWeight;
    [SerializeField] private Transform centerOfMass;

    //Axles
    [Space(20)] [SerializeField] private List<AxleInfo.Axle> axleInfos;

    /*Public*/

    //Car metrics UI
    [NonSerialized] public float RpmMotor;
    [NonSerialized] public float Speed;
    [NonSerialized] public float[] wheelCollidersRpm = new float[4];

    /*Private*/

    //Car variable
    private float _currentBrakeForce;
    private float _currentMotorTorque;
    private float _currentSteeringAngle;

    //Object
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void SetCarRigidbodySettings()
    {
        _rigidbody.mass = carWeight;
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

        private void SetSpringSettings(AxleInfo.Axle axle)
        {
            var leftWheel = axle.leftWheel;
            var leftSpring = leftWheel.suspensionSpring;
            leftSpring.spring = axle.springForce;
            leftSpring.damper = axle.damperValue;
            leftSpring.targetPosition = axle.suspensionSpringPosition;
            leftWheel.suspensionSpring = leftSpring;
            leftWheel.suspensionDistance = axle.suspensionSpringDistance;

            var rightWheel = axle.rightWheel;
            var rightSpring = rightWheel.suspensionSpring;
            rightSpring.spring = axle.springForce;
            rightSpring.damper = axle.damperValue;
            rightSpring.targetPosition = axle.suspensionSpringPosition;
            rightWheel.suspensionSpring = rightSpring;
            rightWheel.suspensionDistance = axle.suspensionSpringDistance;
            wheelCollidersRpm.SetValue(leftWheel.rpm, 1);
    }

    private void HandleMotor()
         {
             _currentMotorTorque = motorTorque * InputManager.verticalAxle;
             _currentSteeringAngle = steeringAngle * InputManager.horizontalAxle;
             _currentBrakeForce = InputManager.isBraking ? brakeForce : 0f;
             _rigidbody.isKinematic = InputManager.isKinematic;
             Speed = _rigidbody.velocity.magnitude * 3.6f;
             RpmMotor = Speed % 30 * 40;
         }

    private void AxleController(AxleInfo.Axle axle)
    {
        if (axle.steering)
        {
            axle.leftWheel.steerAngle = _currentSteeringAngle;
            axle.rightWheel.steerAngle = _currentSteeringAngle;
        }

        if (axle.motor)
        {
            axle.leftWheel.motorTorque = _currentMotorTorque;
            axle.rightWheel.motorTorque = _currentMotorTorque;
        }

        if (!axle.braking) return;
        axle.leftWheel.brakeTorque = _currentBrakeForce;
        axle.rightWheel.brakeTorque = _currentBrakeForce;
    }
    
    private void FixedUpdate()
    {
        SetCarRigidbodySettings();
        HandleMotor();
        foreach (var axle in axleInfos)
        {
            SetSpringSettings(axle);
            AxleController(axle);
            UpdateMeshes.Update(axle);
        }
    }
}