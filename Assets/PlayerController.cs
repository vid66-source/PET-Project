using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Movement")]
    [SerializeField] private float _speed;

    private Vector3 _inputDir;

    [Header("Rotation")]
    [SerializeField] private float _mouseSensitivity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CalculateInputDir();
        LookRotation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void CalculateInputDir()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 dir = transform.right * x + transform.forward * y;
        _inputDir = dir.normalized;
    }

    private void MovePlayer()
    {
        Vector3 targetVelocity = _inputDir * _speed;
        targetVelocity.y = _rb.velocity.y;
        _rb.velocity = targetVelocity;
    }

    private void LookRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
    }
}