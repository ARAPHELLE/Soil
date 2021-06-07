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

    private bool isGrounded;

    private float sensitivity = 1.725f;

    public ParticleSystem landFX;

    private Vector3 cameraPosition;

    [SerializeField] private Transform cameraTransform;
    private Camera cameraComponent;
    private float camOffset;
    private float targetCamOffset;

    private const float groundCheckSize = 0.2f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private CapsuleCollider bodyCollider;
    [SerializeField] private PhysicMaterial nonStickMaterial;

    [SerializeField] private Player _player;

    private float airTime;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraComponent = cameraTransform.GetComponent<Camera>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSize, groundMask);

        _player.grounded = isGrounded;

        float _x = Input.GetAxisRaw("Horizontal");
        float _z = Input.GetAxisRaw("Vertical");

        move = (transform.right * _x + transform.forward * _z).normalized;

        if (_player.CanJump() && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            Vector3 vel = rb.velocity;
            vel.y += _player.jumpForce;
            rb.velocity = vel;
            _player.stamina -= _player.jumpDrain;
        }

        //roll.x = Mathf.Lerp(roll.x, -_x * 1.25f, Time.deltaTime * 3.0f);

        if (_player.CanRun() && Input.GetKey(KeyCode.LeftShift))
        {
            _player.sprinting = true;
            cameraComponent.fieldOfView = Mathf.Lerp(cameraComponent.fieldOfView, 110.0f, Time.deltaTime * 4.0f);
        }
        else
        {
            _player.sprinting = false;
            cameraComponent.fieldOfView = Mathf.Lerp(cameraComponent.fieldOfView, 90.0f, Time.deltaTime * 4.0f);
        }
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
        rb.AddForce(move * _player.speed, ForceMode.Acceleration);

        transform.localEulerAngles = new Vector3(0, yaw, 0);

        if (_player.sprinting)
        {
            _player.stamina -= 12f * Time.fixedDeltaTime;
            _player.speed = _player.runSpeed;
        }
        else
        {
            _player.speed = _player.walkSpeed;
        }

        targetCamOffset = Mathf.Lerp(targetCamOffset, 0.0f, Time.deltaTime * 8.0f);
        camOffset = Mathf.Lerp(camOffset, targetCamOffset, Time.deltaTime * 12.0f);

        CheckLand();
        CheckAirTime();
    }

    private void LateUpdate()
    {
        #region Mouse Input
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);
        #endregion

        #region Camera Position and Rotation
        cameraPosition = new Vector3(transform.position.x, transform.position.y + 0.5f + camOffset, transform.position.z);
        cameraTransform.position = cameraPosition;
        cameraTransform.eulerAngles = new Vector3(pitch + roll.z, yaw, roll.x);
        #endregion
    }

    private void CheckAirTime()
    {
        if (isGrounded)
        {
            airTime = 0f;
        }
        else
        {
            airTime += Time.deltaTime;
        }
    }
    private void CheckLand()
    {
        if (airTime > 0)
        {
            if (isGrounded)
            {
                Debug.Log("Landed");
                targetCamOffset = -0.6f;

                if (airTime > 1f)
                {
                    landFX.Play();
                }
            }
        }
    }
}
