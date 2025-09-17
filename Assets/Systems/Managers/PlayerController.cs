using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Manager References
    private InputManager inputManager => GameManager.Instance.inputManager;
    private CharacterController characterController => GetComponent<CharacterController>();

    [SerializeField] private Transform cameraRoot;
    public Transform CameraRoot => cameraRoot;

    [Header("Enable/Disable Controls & Features")]
    public bool moveEnabled = true;
    public bool lookEnabled = true;

    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private bool sprintEnabled = true;


    [Header("Move Settings")]
    public float moveSpeed = 5;
    [SerializeField] private float crouchMoveSpeed = 2.0f;
    [SerializeField] private float walkMoveSpeed = 4.0f;
    [SerializeField] private float sprintMoveSpeed = 7.0f;

    private float speedTransitionDuration = 0.25f; // Time in seconds for speed transitions
    [SerializeField] private float currentMoveSpeed; // Tracks the current interpolated speed

    private bool sprintInput = false; 
    private bool crouchInput = false;


    [Header("Look Settings")]
    public float horizontalLookSensitivity = 30;
    public float verticalLookSensitivity = 30;
    public float LowerLookLimit = -60;
    public float upperLookLimit = 60;
    public bool invertLookY { get; private set; } = false;

    // Input Variables
    private Vector2 moveInput;
    private Vector2 lookInput;

    private void Awake()
    {
        
    }


    private void Update()
    {
        HandlePlayerMovement();
    }

    private void LateUpdate()
    {
        HandlePlayerLook();
    }


    public void HandlePlayerMovement()
    {
        if (moveEnabled == false) return; // Check if movement is enabled

        // Step 1: Get input direction
        Vector3 moveInputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldMoveDirection = transform.TransformDirection(moveInputDirection);

        // Step 2: Determine movement speed
        float targetMoveSpeed;

        if (sprintInput == true)
        {
            targetMoveSpeed = sprintMoveSpeed;
        }
        else
        {
            targetMoveSpeed = walkMoveSpeed;
        }


        // Step 3: Smoothly interpolate current speed towards target speed
        float lerpSpeed = 1f - Mathf.Pow(0.01f, Time.deltaTime / speedTransitionDuration);
        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, lerpSpeed);


        // Step 4: Handle horizontal movement
        Vector3 horizontalMovement = worldMoveDirection * currentMoveSpeed;

        // Step 5: Handle jumping and gravity

        // Step 6: Combine horizontal and vertical movement
        Vector3 movement = horizontalMovement;

        // Step 7: Apply final movement
        characterController.Move(movement * Time.deltaTime);
    }

   public void HandlePlayerLook()
   {
        if (lookEnabled == false) return; // Check if look is enabled 

        float lookX = lookInput.x * horizontalLookSensitivity * Time.deltaTime;
        float lookY = lookInput.y * verticalLookSensitivity * Time.deltaTime;

        // Invert vertical look if needed
        if (invertLookY)
        {
            lookY = -lookY;
        }

        // Rotate character on Y-axis (left/right look)
        transform.Rotate(Vector3.up * lookX);

        // Tilt cameraRoot on X-axis (up/down look)
        Vector3 currentAngles = cameraRoot.localEulerAngles;
        float newRotationX = currentAngles.x - lookY;

        // Convert to signed angle for proper clamping
        newRotationX = (newRotationX > 180) ? newRotationX - 360 : newRotationX;
        newRotationX = Mathf.Clamp(newRotationX, LowerLookLimit, upperLookLimit);

        CameraRoot.localEulerAngles = new Vector3(newRotationX, 0, 0);

    }




    #region Input Methods

    void SetMoveInput(Vector2 inputVector)
    {
       moveInput = new Vector2(inputVector.x, inputVector.y);
    }

    void SetLookInput(Vector2 inputVector)
    {
        lookInput = new Vector2(inputVector.x, inputVector.y);
    }


    void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump Input Started");
            // Handle jump start logic here
        }
    }

    void HandleCrouchInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Crouch Input Started");
            // Handle jump start logic here
        }
    }

    void HandleSprintInput(InputAction.CallbackContext context)
    {
        // if Sprint is not enabled, do nothing and just return
        if (sprintEnabled == false) return;

        if (context.started)        
        { 
            sprintInput = true;             
        }
        else if (context.canceled) 
        { 
            sprintInput = false;             
        }

    }




    #endregion





    void OnEnable()
    {
        inputManager.MoveInputEvent += SetMoveInput;
        inputManager.LookInputEvent += SetLookInput;

        inputManager.JumpInputEvent += HandleJumpInput;
        inputManager.CrouchInputEvent += HandleCrouchInput;
        inputManager.SprintInputEvent += HandleSprintInput;

    }

    void OnDestroy()
    {
        inputManager.MoveInputEvent -= SetMoveInput;
        inputManager.LookInputEvent -= SetLookInput;

        inputManager.JumpInputEvent -= HandleJumpInput;
        inputManager.CrouchInputEvent += HandleCrouchInput;
        inputManager.SprintInputEvent += HandleSprintInput;


    }



}
