using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    // REFERENCES
    [SerializeField] public UI_Manager uiManager;
    [SerializeField] private CombatantInfo playerInfo;
    [SerializeField] private EnemyDetection enemyDetection;

    // CAMERA
    [SerializeField] public Transform playerHead;

    [SerializeField] private CinemachineVirtualCamera cameraView;

    public CinemachineBasicMultiChannelPerlin camShake;
    public float camShakeAmp;

    [SerializeField] public float MoveSpeed = 1.0f;
    [SerializeField] public float SprintSpeed = 1.0f;



    [SerializeField] public float RotationSpeed = 10f;

    public float horizontalFacing = 0f;
    public float verticalFacing = 0f;

    public float defaultMoveSpeed = 0f;

    // PHYSICS
    public Rigidbody physicsBody;

    // GAME
    public bool isPaused = false;
    public bool isDying = false;

    // collisions
    private RoomBase currentRoom = null;
    private PhysicsDoor currentDoor = null;

    // aim 
    private float defaultFOV = 60f;
    private float currentFOV = 0f;
    private float aimFOV = 45f;
    private float aimTime = 0.1f;
    private float aimTimer = 0f;

    // minimap
    private Camera minimapCam;
    private int minimapNoMarker = 1 << 6;
    private int minimapWithMarker = 1 << 6 | 1 << 8;

    private void Start()
    {
        // init rigidbody
        physicsBody = GetComponent<Rigidbody>();

        // setup cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // freeze rigidbody rotation
        physicsBody.freezeRotation = true;

        // clear prompt
        uiManager.DeactivatePrompt();
        uiManager.SetReticle(0);

        // assign shake
        camShake = cameraView.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        camShakeAmp = 0f;

        // assign minimap
        minimapCam = transform.GetChild(0).GetComponent<Camera>();
        minimapCam.cullingMask = minimapNoMarker;

        // assign enemy detection
        enemyDetection = GetComponent<EnemyDetection>();
    }

    private void Update()
    {
        if (!uiManager.inCombat && !isDying)
        {
            MoveCameraWithMouse();
        }

        CheckInput();
    }

    public void SetReferences(UI_Manager uiReference, CombatantInfo playerInfoReference)
    {
        uiManager = uiReference;
        playerInfo = playerInfoReference;
    }

    private void CheckInput()
    {

        // GETKEY (hold)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift); // PRESS LEFT SHIFT
        Sprint(isSprinting);

        bool isScanning = Input.GetKey(KeyCode.Space); // PRESS SPACE
        Scan(isScanning);

        bool isAiming = Input.GetKey(KeyCode.Mouse1); // RIGHT CLICK
        Aim(isAiming);

        // GETKEYDOWN (tap)

        bool isInteracting = Input.GetKeyDown(KeyCode.E); // PRESS E
        Interact(isInteracting);

        bool isExiting = Input.GetKeyDown(KeyCode.Escape); // PRESS ESCAPE
        Pause(isExiting);

        bool testInteract = Input.GetKeyDown(KeyCode.V); // PRESS V
        Test(testInteract);


    }

    private void FixedUpdate()
    {
        MoveWithPhysics();
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


        private void Interact(bool isInteracting)
          {
        if (isInteracting)
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

    private void Sprint(bool isSprinting)
    {
        if (isSprinting)
        {
            MoveSpeed = SprintSpeed;
        }
        else
        {
            MoveSpeed = defaultMoveSpeed;
        }

    }

    private void Pause(bool isExiting)
    {
        if (isExiting)
        {
            if (!isPaused)
            {
                uiManager.ActivatePause();
                isPaused = true;
            }
            else
            {
                uiManager.ActivateHUD();
                isPaused = false;
            }

        }

    }

    private void Test(bool isTesting)
    {
        if (isTesting)
        {
            if (!isPaused)
            {
                uiManager.ActivateWeapon();

                isPaused = true;
            }
            else
            {
                uiManager.ActivateHUD();
                isPaused = false;
            }

        }

    }

    private void Scan(bool isScanning)
    {
        if (isScanning)
        {
            minimapCam.cullingMask = minimapWithMarker;
            uiManager.radarText.color = Color.green;

            uiManager.radarText.text = "Scanning ...";


        }
        else
        {

            minimapCam.cullingMask = minimapNoMarker;

            uiManager.radarText.color = Color.grey;
            uiManager.radarText.text = "[space] to scan.";
        }

    }

    private void Aim(bool isAiming)
    {
        if (isAiming) // if is aiming
        {
            currentFOV = Mathf.Lerp(defaultFOV, aimFOV, aimTimer / aimTime);
            aimTimer += Time.deltaTime;
            cameraView.m_Lens.FieldOfView = currentFOV;

            if (aimTime < aimTimer)
            {
                cameraView.m_Lens.FieldOfView = aimFOV;
            }


            uiManager.SetReticle(1);

        }
        else
        {
            cameraView.m_Lens.FieldOfView = defaultFOV;
            aimTimer = 0f;

            uiManager.SetReticle(0);
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyDetection.inRangeOfEnemy = false;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyDetection.currentEnemy = other.transform.GetComponentInParent<EnemyAI>();
            enemyDetection.inRangeOfEnemy = true;
        }
    }

    public void Die()
    {
        // destroy detection
        Destroy(enemyDetection);
        isDying = true;

        Rigidbody physicsHead = playerHead.gameObject.AddComponent<Rigidbody>();

        physicsHead.AddTorque(Vector3.up * 0.1f, ForceMode.Impulse);
    }

}
