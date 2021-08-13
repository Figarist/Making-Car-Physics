using System.ComponentModel;
using UnityEngine;

public class AxleInfo
{
    [System.Serializable]
    public class Axle
    {
        /*Wheels*/
        [Header("Wheels")] public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public Transform leftWheelMesh;
        public Transform rightWheelMesh;

        /*Spring*/
        [Header("Suspension Spring")] public int springForce;
        public int damperValue;
        [Range(0, 2), DefaultValue(0.3)] public float suspensionSpringDistance;
        [Range(0, 1), DefaultValue(0.5)] public float suspensionSpringPosition;

        /*Car controls*/
        [Header("Car controls")] public bool motor;
        public bool steering;
        public bool braking;
    }
}