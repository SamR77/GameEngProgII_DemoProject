using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class InteractionManager : MonoBehaviour
{
    // Manager References
    private InputManager inputManager => GameManager.Instance.InputManager;




    [Header("Interaction Settings")]
    private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 3f;


    public string DebugCurrentInteractable;

 

    [Header("Interaction Cooldown")]
    [Tooltip("Time in seconds before the player can interact again after a successful interaction. Prevents multiple nteractions on one press")]
    [SerializeField] private float interactionCooldown = 0.1f; // seconds
    private float lastInteractionTime = -Mathf.Infinity;


    // Interface reference used internally
    private IInteractable currentFocusedInteractable;

    private Transform cameraRoot; // Reference to the player's camera root transform


    private void Start()
    {
        // Set the interactable layer
        interactableLayer = LayerMask.GetMask("Interactable");

        // Set the camera root from the player controller
        cameraRoot = GameManager.Instance.PlayerController.CameraRoot;

    }


    private void Update()
    {
        HandleInteractionDetection();
    }

    private void HandleInteractionDetection()
    {
        if (Physics.Raycast(cameraRoot.transform.position, cameraRoot.transform.forward, out RaycastHit hitInfo, interactionDistance, interactableLayer))
        {
            // Debug.Log($"Raycast hit object: " +hitInfo.collider.name);

            // Get the interactable component from the hit object
            IInteractable hitInteractable = hitInfo.collider.GetComponent<IInteractable>();

            if (hitInteractable != null)
            {
                // If it's different from our current focus
                if (hitInteractable != currentFocusedInteractable)
                {
                    // 1. Clear previous focus if we had one
                    if (currentFocusedInteractable != null)
                    {
                        currentFocusedInteractable.SetFocus(false);
                    }

                    // 2. Set new focus
                    currentFocusedInteractable = hitInteractable;
                    currentFocusedInteractable.SetFocus(true);

                    // 3. Get the prompt text from interactable and tell the UI to show it

                    // use reference to UI text to pass through Interact Prompt


                }
            } 
        }
        else if (currentFocusedInteractable != null)
        {
            currentFocusedInteractable.SetFocus(false);
            currentFocusedInteractable = null;

            DebugCurrentInteractable = null;
        }

    }

    private void OnInteractInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Cooldown check to prevent spamming interactions
        if (Time.time - lastInteractionTime < interactionCooldown)
            return; // Still cooling down


        if (context.performed)
        {
            if (currentFocusedInteractable != null)
            {
                currentFocusedInteractable.OnInteract();
            }
        }
       

    }


    private void OnEnable()
    {
        inputManager.InteractInputEvent += OnInteractInput;
    }

    private void OnDestroy()
    {
        inputManager.InteractInputEvent -= OnInteractInput;
    }


}
