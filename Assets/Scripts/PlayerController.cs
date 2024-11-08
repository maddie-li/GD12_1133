using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1.0f;
    [SerializeField] private float SprintSpeed = 1.0f;
    [SerializeField] private float RotationSpeed = 10f;

    [SerializeField] private Transform PlayerHead;

    private Rigidbody physicsBody;

    private float horizontalFacing = 0f;
    private float verticalFacing = 0f;

    private float defaultMoveSpeed = 0f;

    private RoomBase currentRoom = null;

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

    }

    private void Update()
    {
        MoveCameraWithMouse();
        CheckIfSprinting();
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
        PlayerHead.localRotation = Quaternion.Euler(verticalFacing, 0f, 0f);
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

    private void CheckIfSprinting()
    {
        float isSprinting = Input.GetAxis("Sprint");

        if (isSprinting > 0)
        {
            MoveSpeed = SprintSpeed;
        }
        else
        {
            MoveSpeed = defaultMoveSpeed;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        currentRoom = other.GetComponent <RoomBase>();
        currentRoom.OnRoomEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        currentRoom = other.GetComponent<RoomBase>();
        currentRoom.OnRoomExit();
    }

    private void OnTriggerStay(Collider other)
    {
        currentRoom = other.GetComponent<RoomBase>();
        currentRoom.OnRoomStay();
    }

}
