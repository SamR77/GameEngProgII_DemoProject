using System.Collections;
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
        SceneManager.sceneLoaded += OnSceneLoaded;        

        SceneManager.LoadScene(sceneId);

        if (sceneId == 0) // Loading Main Menu
        {
            gameStateManager.SwitchToState(gameStateManager.gameState_MainMenu);
        }
        else // it should be a Gameplay level
        {
            gameStateManager.SwitchToState(gameStateManager.gameState_Gameplay);
        }        
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        gameStateManager.SwitchToState(gameStateManager.gameState_Loading);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        // Update Progress bar during loading operation.
        while (asyncLoad.isDone == false)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            uIManager.LoadingUIController.UpdateProgressBar(progressValue);               
            yield return null;
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

    public void LoadMainMenu()
    {
        LoadScene(0);

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
