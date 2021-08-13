using UnityEngine;

public class UpdateMeshes
{
    public static void Update(AxleInfo.Axle axle)
    {
        UpdateSingleMesh(axle.leftWheel, axle.leftWheelMesh);
        UpdateSingleMesh(axle.rightWheel, axle.rightWheelMesh);
    }

    private static void UpdateSingleMesh(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out var position, out var rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }
}
