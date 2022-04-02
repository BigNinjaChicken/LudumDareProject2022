using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Mouse")]
    public float sensXCam;
    public float sensYCam;

    public Transform orientationCam;

    float xRotationCam;
    float yRotationCam;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;
    float jumpheld = 0;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientationPlayer;

    float horizontalInput = 0;
    float verticalInput = 0;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        }

        // Jump Handle
        if (jumpheld == 1 && readyToJump && grounded)
        {
            readyToJump = false;

            doJump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void Look(InputAction.CallbackContext context)
    {
        float mouseX = context.ReadValue<Vector2>().x * sensXCam;
        float mouseY = context.ReadValue<Vector2>().y * sensYCam;

        yRotationCam += mouseX;

        xRotationCam -= mouseY;
        xRotationCam = Mathf.Clamp(xRotationCam, -90f, 90f);

        // rotate cam and orientation
        orientationCam.rotation = Quaternion.Euler(xRotationCam, yRotationCam, 0);
        orientationPlayer.rotation = Quaternion.Euler(0, yRotationCam, 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpheld = context.ReadValue<float>();
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientationPlayer.forward * verticalInput + orientationPlayer.right * horizontalInput;
        
        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed) // if goes over moveSpeed
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; // sets it to what its supposed to be
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); // applies force
        }
    }

    private void doJump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
