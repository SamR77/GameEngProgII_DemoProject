using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    private UIDocument mainMenuUIDoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;
    UIManager UIManager => GameManager.Instance.UIManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;


    Button playButton;
    Button optionsButton;
    Button quitButton;

    private Button[] menuButtons;




    private void OnEnable()
    {
        // Button References
        playButton = mainMenuUIDoc.rootVisualElement.Q<Button>("PlayButton");
        optionsButton = mainMenuUIDoc.rootVisualElement.Q<Button>("OptionsButton");
        quitButton = mainMenuUIDoc.rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        optionsButton.clicked += OnOptionsClicked;
        quitButton.clicked += OnQuitButtonClicked;



        // Check to make sure buttons are found
        if (playButton == null) Debug.LogError("Play Button not found in MainMenu_UIDoc");
        if (optionsButton == null) Debug.LogError("Options Button not found in MainMenu_UIDoc");
        if (quitButton == null) Debug.LogError("Quit Button not found in MainMenu_UIDoc");




    }

    private void OnDestroy()
    {

        playButton.clicked -= OnPlayButtonClicked;
        optionsButton.clicked -= OnOptionsClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    #region Button Click Handlers
    private void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Quit Clicked - Application.Quit() called");
    }

    private void OnOptionsClicked()
    {
        Debug.Log("Options Clicked - Not Implemented Yet");
    }

    private void OnPlayButtonClicked()
    {
        Debug.Log("Play Clicked - Loading Level 1");

        levelManager.LoadScene(1);
    }
    
    #endregion


}
