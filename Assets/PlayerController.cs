using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _col;

    [Header("Movement")] [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Vector3 _inputDir;
    private bool _jumpRequested;

    [Header("Rotation")] [SerializeField] private float _mouseSensitivity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb == null)
            Debug.LogError("No Rigidbody attached.");
        _col = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        CalculateInputDir();
        LookRotation();
        _jumpRequested = IsGrounded() && Input.GetKeyDown(KeyCode.Space);
        Debug.DrawRay(_col.bounds.center, Vector3.down * (_col.bounds.extents.y + 0.1f), Color.red);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
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

    private bool IsGrounded()
    {
        float rayStart = _col.bounds.center.y;
        float rayLength = _col.bounds.extents.y + 0.1f;
        return Physics.Raycast(new Vector3(transform.position.x, rayStart, transform.position.z), Vector3.down, rayLength);
    }

    public void Jump()
    {
        if (_jumpRequested)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _jumpRequested = false;
        }
    }
}