using UnityEngine;


// GameManager must load first to initialize its references before sub-managers
[DefaultExecutionOrder(-100)]

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Manager References")]
    public InputManager inputManager;



    private void Awake()
    {
        #region Singleton
        // Singleton pattern to ensure only one instance of GameManager exists

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
        #endregion


        // check and set all references to the managers
        if (inputManager == null) { inputManager = GetComponentInChildren<InputManager>(); }




    }





}
