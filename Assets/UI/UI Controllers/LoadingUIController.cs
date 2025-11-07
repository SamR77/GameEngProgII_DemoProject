using UnityEngine;
using UnityEngine.UIElements;

public class LoadingUIController : MonoBehaviour
{
    GameManager gameManager;
    UIManager uIManager;
    LevelManager levelManager;
    InputManager inputManager; 
    GameStateManager gameStateManager;

    UIDocument loadingUIDoc;
    ProgressBar progressBar;

    private void Awake()
    {
        #region Set Manager References

        // Set Managers References ( "??=" if not already set)
        gameManager ??= GameManager.Instance;
        uIManager ??= GameManager.Instance.UIManager;
        levelManager ??= GameManager.Instance.LevelManager;
        inputManager ??= GameManager.Instance.InputManager;
        gameStateManager ??= GameManager.Instance.GameStateManager;

        //check manager references for null
        if (gameManager == null) Debug.LogError("GameManager reference is null!");
        if (uIManager == null) Debug.LogError("UIManager reference is null!");
        if (levelManager == null) Debug.LogError("LevelManager reference is null!");
        if (inputManager == null) Debug.LogError("InputManager reference is null!");
        if (gameStateManager == null) Debug.LogError("GameStateManager reference is null!");

        #endregion
    }

    // Start() call is reccomended for setting UItoolkit references
    private void Start()
    {
        #region Set UI References

        // Set UI Document Reference ( "??=" if not already set)
        loadingUIDoc ??= GetComponent<UIDocument>();
        if (loadingUIDoc == null) Debug.LogError("No UIDocument component found on this gameobject!");

        // Set ProgressBar ( "??=" if not already set)
        progressBar ??= loadingUIDoc.rootVisualElement.Q<ProgressBar>("ProgressBar");
        if (progressBar == null) Debug.LogError("ProgressBar not found in LoadingUI Doc");

        #endregion

    }

    public void UpdateProgressBar(float progress)
    {
        // progress bar is a value between 0 and 1
        progressBar.value = progress;

        // Shows 0-100% (just the integer part)
        progressBar.title = $"{(int)(progress * 100)}%";
    }

}
