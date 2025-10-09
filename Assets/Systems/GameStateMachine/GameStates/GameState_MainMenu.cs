using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_MainMenu : IState
{
    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    PlayerController playerController => GameManager.Instance.PlayerController;
    UIManager uIManager => GameManager.Instance.UIManager;

    #region Singleton Instance
    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_MainMenu instance = new GameState_MainMenu();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_MainMenu Instance = instance;
    #endregion



    public void EnterState()
    {
        Debug.Log("Entered Main Menu State");

        Time.timeScale = 0f; // Pause the game

        Cursor.visible = true;


        uIManager.ShowMainMenu();



 

    }

    public void FixedUpdateState()
    {
        
    }

    public void UpdateState()
    {



    }

    public void LateUpdateState()
    {
        
    }

    public void ExitState()
    {
        Debug.Log("Exiting Main Menu State");
    }

}
