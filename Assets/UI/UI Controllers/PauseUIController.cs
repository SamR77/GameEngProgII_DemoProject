using UnityEngine;
using UnityEngine.UIElements;

public class PauseUIController : MonoBehaviour
{
    private UIDocument pauseUIDoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;
    UIManager UIManager => GameManager.Instance.UIManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;


    Button resumeButton;
    Button mainMenuButton;
    Button quitButton;

    private Button[] menuButtons;




    private void OnEnable()
    {
        // Button References
        resumeButton = pauseUIDoc.rootVisualElement.Q<Button>("ResumeButton");
        mainMenuButton = pauseUIDoc.rootVisualElement.Q<Button>("MainMenuButton");
        quitButton = pauseUIDoc.rootVisualElement.Q<Button>("QuitButton");

        resumeButton.clicked += OnResumeButtonClicked;
        mainMenuButton.clicked += OnMainMenuButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;



        // Check to make sure buttons are found
        if (resumeButton == null) Debug.LogError("Resume Button not found in PauseMenuUI Doc");
        if (mainMenuButton == null) Debug.LogError("MainMenu Button not found in PauseMenuUI Doc");
        if (quitButton == null) Debug.LogError("Quit Button not found in PauseMenuUI Doc");




    }

    private void OnDestroy()
    {

        resumeButton.clicked -= OnResumeButtonClicked;
        mainMenuButton.clicked -= OnMainMenuButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    #region Button Click Handlers

    private void OnResumeButtonClicked()
    {
        Debug.Log("Resume Clicked");

        gameStateManager.Resume();
    }

    private void OnMainMenuButtonClicked()
    {
        Debug.Log("MainMenu Clicked");

        levelManager.LoadMainMenu();
    }



    private void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Quit Clicked - Application.Quit() called");
    }



 

    #endregion


}
