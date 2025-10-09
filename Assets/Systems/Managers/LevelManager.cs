using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int nextScene;

    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    PlayerController playerController => GameManager.Instance.PlayerController;
    UIManager uIManager => GameManager.Instance.UIManager;


    public void LoadScene(int sceneId)
    {
        //playerController.playerSpawnpoints.Clear(); // Clear the spawnpoints list to avoid duplicates

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneId);

        if (sceneId == 0) // Loading Main Menu
        {
            gameStateManager.SwitchToState(gameStateManager.gameState_MainMenu);
        }
        else // it should be a Gameplay level
        {
            // Note: Might need a check here if we ever have a level change while in the State_GameRidecar

            gameStateManager.SwitchToState(gameStateManager.gameState_Gameplay);
        }
    }


    public void LoadNextLevel()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene <= SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(nextScene);
        }

        else if (nextScene > SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("All levels complete!");
        }
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // perform any functions that need to happen ONLY AFTER a scene is done loading


        playerController.MovePlayerToSpawnpoint("StartPosition");


        // Unsubscribe from the event to avoid multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
