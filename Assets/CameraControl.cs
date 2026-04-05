using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField]private float _mouseSensitivity  = 5f;
    private float _cameraPitch = 0f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") *  _mouseSensitivity;
        _cameraPitch  -= mouseY;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
        transform.localRotation = Quaternion.AngleAxis(_cameraPitch, Vector3.right);
    }
}
