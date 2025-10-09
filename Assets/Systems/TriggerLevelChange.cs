using UnityEngine;

public class TriggerLevelChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadNextLevel();
        }
    }
}
