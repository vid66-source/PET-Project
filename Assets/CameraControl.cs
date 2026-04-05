using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float _mouseSensitivity;

    private float _cameraPitch;

    private void Update()
    {
        LookRotation();
    }

    private void LookRotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _cameraPitch -= mouseY;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(_cameraPitch, Vector3.right);
    }
}