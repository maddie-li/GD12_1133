/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {

        // set default move speed
        defaultMoveSpeed = MoveSpeed;
    }

    public void SetPlayerController(PlayerController newPlayerController)
    {
        playerController =
    }


    public void MoveCameraWithMouse()
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

        uiManager.UpdateRadar(horizontalFacing);



    }

    public void MoveWithPhysics()
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

        // do camera shake effect
        camShakeAmp = velocity.magnitude;


        if (velocity.magnitude == 0) // for idle
        {
            camShake.m_AmplitudeGain = 0.9f;
            camShake.m_FrequencyGain = 0.25f;
        }
        else // moving
        {
            camShake.m_AmplitudeGain = camShakeAmp / 12;
            camShake.m_FrequencyGain = camShakeAmp / 5;
        }


    }
}*/