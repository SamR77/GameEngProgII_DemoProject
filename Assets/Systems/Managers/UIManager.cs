using UnityEngine;
using UnityEngine.UIElements;   

public class UIManager : MonoBehaviour
{
    [Header("UI Menu Objects")]
    [SerializeField] private UIDocument mainMenuUI;
    [SerializeField] private UIDocument gameplayUI;
    [SerializeField] private UIDocument pauseUI;

    private void Awake()
    {
        mainMenuUI = FindUIDocument("MainMenuUI");
        gameplayUI = FindUIDocument("GameplayUI");
        pauseUI = FindUIDocument("PauseUI");

        // Activate Parent GameObject of all UI Screens (Some are disbaled for visibity in the editor Game view)
        if (mainMenuUI != null) mainMenuUI.gameObject.SetActive(true);
        if (gameplayUI != null) gameplayUI.gameObject.SetActive(true);
        if (pauseUI != null) pauseUI.gameObject.SetActive(true);

        HideAllUIMenus();
    }


    
    public void ShowMainMenu()
    {
        Debug.Log("UIManager: ShowMainMenu called.");

        HideAllUIMenus();

        mainMenuUI.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    public void ShowPauseMenu()
    {
        HideAllUIMenus();
        pauseUI.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void ShowGameplayUI()
    {
        HideAllUIMenus();
        gameplayUI.rootVisualElement.style.display = DisplayStyle.Flex;
    }


    public void HideAllUIMenus()
    {
        if (mainMenuUI == null) Debug.LogError("mainMenuUI is null, please check the UIManager setup.");
        if (pauseUI == null) Debug.LogError("pausedUI is null, please check the UIManager setup.");
        if (gameplayUI == null) Debug.LogError("gameplayUI is null, please check the UIManager setup.");

        mainMenuUI.rootVisualElement.style.display = DisplayStyle.None;
        gameplayUI.rootVisualElement.style.display = DisplayStyle.None;
        pauseUI.rootVisualElement.style.display = DisplayStyle.None;
    }



    private UIDocument FindUIDocument(string name)
    {
        var documents = Object.FindObjectsByType<UIDocument>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var doc in documents)
        {
            if (doc.name == name)
            {
                return doc;
            }
        }
        Debug.LogWarning($"UIDocument '{name}' not found in scene.");
        return null;
    }






}
