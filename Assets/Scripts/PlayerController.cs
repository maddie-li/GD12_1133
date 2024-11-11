using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1.0f;
    [SerializeField] private float SprintSpeed = 1.0f;
    [SerializeField] private float RotationSpeed = 10f;

    [SerializeField] private Transform playerHead;
    [SerializeField] private Camera cameraView;

    [SerializeField] private UI_Manager uiManager;

    private Rigidbody physicsBody;

    private float horizontalFacing = 0f;
    private float verticalFacing = 0f;

    private float defaultMoveSpeed = 0f;

    // aim 
    private float defaultFOV = 60f;
    private float currentFOV = 0f;
    private float aimFOV = 45f;
    private float aimTime = 0.1f;
    private float aimTimer = 0f;

    // collisions
    private RoomBase currentRoom = null;
    private PhysicsDoor currentDoor = null;

    private void Start()
    {
        // init rigidbody
        physicsBody = GetComponent<Rigidbody>();

        // setup cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // freeze rigidbody rotation
        physicsBody.freezeRotation = true;

        // set default move speed
        defaultMoveSpeed = MoveSpeed;

        // clear prompt
        uiManager.DeactivatePrompt();

    }

    private void Update()
    {
        MoveCameraWithMouse();
        CheckInput();
    }

    public void SetUIReference(UI_Manager reference)
    {
        uiManager = reference;
    }

    private void CheckInput()
    {
        float isInteracting = Input.GetAxis("Interact"); // PRESS E
        Interact(isInteracting);

        float isSprinting = Input.GetAxis("Sprint"); // PRESS LEFT SHIFT
        Sprint(isSprinting);
        

        float isAiming = Input.GetAxis("Fire2");
        Aim(isAiming);
    }

    private void FixedUpdate()
    {
        MoveWithPhysics();
    }

    private void MoveCameraWithMouse()
    {
        // get mouse input
        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;

        // calculate and clamp pitch
        verticalFacing -= mouseY;
        verticalFacing = Mathf.Clamp(verticalFacing, -90f, 90f);

        horizontalFacing += mouseX;

        // make vertical movement camera
        playerHead.localRotation = Quaternion.Euler(verticalFacing, 0f, 0f);
        // make horizontal movement body
        physicsBody.rotation = Quaternion.Euler(0f, horizontalFacing, 0f);

    }

    private void MoveWithPhysics()
    {
        // get wasd
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // calculate direction relative to orientation
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // apply velocity movement to rigidbody
        Vector3 velocity = moveDirection * MoveSpeed;
        // preserve y velocity
        physicsBody.velocity = new Vector3(velocity.x, physicsBody.velocity.y, velocity.z);

    }

    private void Interact(float isInteracting)
    {
        if (isInteracting > 0)
        {
            if (currentDoor != null) // unlocks door
            {
                currentDoor.Lock(false);
            }
            else if (currentRoom != null) // displays room search
            {
                currentRoom.SearchRoom();
            }
        }
    }

    private void Sprint(float isSprinting)
    {
        if (isSprinting > 0)
        {
            MoveSpeed = SprintSpeed;
        }
        else
        {
            MoveSpeed = defaultMoveSpeed;
        }

    }

    private void Aim(float isAiming)
    {
        if (isAiming > 0) // if is aiming
        {
            currentFOV = Mathf.Lerp(defaultFOV, aimFOV, aimTimer / aimTime);
            aimTimer += Time.deltaTime;
            cameraView.fieldOfView = currentFOV;

            if (aimTime < aimTimer)
            {
                cameraView.fieldOfView = aimFOV;
            }

        }
        else
        {
            cameraView.fieldOfView = defaultFOV;
            aimTimer = 0f;
        }
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            currentDoor = other.transform.parent.GetComponent<PhysicsDoor>();
            if (currentDoor.isLocked)
            {
                uiManager.ActivateDoorPrompt();
            }
            
        }
        if (other.gameObject.CompareTag("Room"))
        {
            currentRoom = other.GetComponent<RoomBase>();
            currentRoom.OnRoomEnter();
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            currentDoor = null;
            uiManager.DeactivatePrompt();
        }
        if (other.gameObject.CompareTag("Room"))
        {
            currentRoom = null;
        }

    }

}
