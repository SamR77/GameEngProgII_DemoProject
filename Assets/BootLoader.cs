using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public static class PerformBootload
{
    const string sceneName = "BootLoader";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {        
       if (SceneManager.GetActiveScene().name != sceneName)
       {
            // Check all currently loaded scenes to see if the bootstrap scene is already loaded
            for(int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var candidateScene = SceneManager.GetSceneAt(sceneIndex);

                if(candidateScene.name == sceneName)
                {
                    // The bootstrap scene is already loaded, no need to load it again
                    return;
                }
            }
            Debug.Log("Loading BootLoader scene" + sceneName);

            // If we get here, the bootstrap scene is not loaded, so load it (additively)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

            // Run any resets or initializations needed before other scenes load
        }
    }
}


public class BootLoader : MonoBehaviour
{
    public static BootLoader Instance { get; private set; } = null;

    private void Awake()
    {
        #region Singleton
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance != null)
        {
            Debug.LogWarning("Another instance of BootLoader already exists. Destroying this one.");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        #endregion
    }

    public void Test()
    {
        Debug.Log("BootLoader Scene is ACTIVE.");
    }

}



