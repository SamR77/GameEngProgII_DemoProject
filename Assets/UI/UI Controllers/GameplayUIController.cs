using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUIController : MonoBehaviour
{
    private UIDocument gameplayUIDoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;
    UIManager UIManager => GameManager.Instance.UIManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;





}
