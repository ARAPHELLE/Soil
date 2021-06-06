using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private float drag = 0.9f;

    private float pitch, yaw;

    private Vector3 roll;

    private Vector3 move;

    private float jumpForce = 10.0f;

    private float speed = 40.0f;

    private bool isGrounded;

    private float sensitivity = 1.725f;

    private Vector3 cameraPosition;

    [SerializeField] private Transform cameraTransform;
    
    private const float groundCheckSize = 0.2f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private CapsuleCollider bodyCollider;
    [SerializeField] private PhysicMaterial nonStickMaterial;

    [SerializeField] private Player _player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSize, groundMask);

        float _x = Input.GetAxisRaw("Horizontal");
        float _z = Input.GetAxisRaw("Vertical");

        move = (transform.right * _x + transform.forward * _z).normalized;

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            Vector3 vel = rb.velocity;
            vel.y += jumpForce;
            rb.velocity = vel;
        }

        roll.x = Mathf.Lerp(roll.x, -_x * 1.25f, Time.deltaTime * 3.0f);
    }

    private void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        vel.x *= drag; vel.z *= drag;

        if (!isGrounded && rb.useGravity == true)
            vel.y -= 0.3f;

        if (move != Vector3.zero)
        {
            bodyCollider.material = nonStickMaterial;
        }
        else
            bodyCollider.material = null;

        rb.velocity = vel;
        rb.AddForce(move * speed, ForceMode.Acceleration);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
    }

    private void LateUpdate()
    {
        #region Mouse Input
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);
        #endregion

        #region Camera Position and Rotation
        cameraPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        cameraTransform.position = cameraPosition;
        cameraTransform.eulerAngles = new Vector3(pitch + roll.z, yaw, roll.x);
        #endregion
    }
}
