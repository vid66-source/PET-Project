using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Movement Settings")]
    [SerializeField]private float _speed = 5f;
    private Vector3 _inputVector;

    [Header("Look Settings")]
    [SerializeField]private float _mouseSensitivity = 4f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _inputVector = (transform.right * x + transform.forward * y).normalized;

        PlayerYawRotation();

    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 targetVelocity = _inputVector  * _speed;
        targetVelocity.y = _rb.velocity.y;
        _rb.velocity = targetVelocity;
    }

    private void PlayerYawRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
    }
}