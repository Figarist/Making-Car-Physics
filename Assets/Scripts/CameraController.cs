using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private SimpleCarController simpleCarController;
    [SerializeField] private GameObject cameraFolder;
    [SerializeField] private Transform[] cameraLocations;
    [SerializeField] [Range(0, 1)] private float smoothTime = 0.5f;
    private int _locationIndicator = 1;
    

    private void Start()
    {
        cameraLocations = cameraFolder.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_locationIndicator >= 4 || _locationIndicator < 1) _locationIndicator = 1;
            else _locationIndicator++;
        }
        var tr = transform;
        tr.position = cameraLocations[_locationIndicator].position * (1 - smoothTime) + tr.position*smoothTime;
        tr.LookAt(cameraLocations[1].position);

        smoothTime = simpleCarController.Speed >= 150 ? Mathf.Abs(simpleCarController.Speed / 150) * 0.85f : .45f;
    }
}
