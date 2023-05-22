using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Deduct a pick from the player's collection
            gameManager.DeductPick();
        }
    }
}
