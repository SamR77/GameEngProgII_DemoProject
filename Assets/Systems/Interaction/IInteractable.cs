using UnityEngine;

public interface IInteractable
{    
    // Called when the player interacts with this object.
    void OnInteract();

    // Sets the focus state of the object.
    void SetFocus(bool focused);

    // Gets the text to display in the UI when this object is focused.
    // Default interface method for getting the interaction prompt.
    // Derived classes can override this if they need a different prompt.
    string GetInteractionPrompt();
}
