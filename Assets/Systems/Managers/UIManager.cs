using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject gameplayUI;
    public GameObject pauseUI;

    private void Awake()
    {
        HideAllUIMenus();
    }


    
    public void ShowMainMenu()
    {
        HideAllUIMenus();
        mainMenuUI.SetActive(true);
    }
    public void ShowPauseMenu()
    {
        HideAllUIMenus();
        pauseUI.SetActive(true);
    }

    public void ShowGameplayUI()
    {
        HideAllUIMenus();
        gameplayUI.SetActive(true);
    }


    public void HideAllUIMenus()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(false);
        pauseUI.SetActive(false);
    }





}
