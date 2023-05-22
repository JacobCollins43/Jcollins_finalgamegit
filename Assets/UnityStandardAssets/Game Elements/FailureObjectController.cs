using UnityEngine;

public class FailureObjectController : MonoBehaviour
{
    // Reference to the GameManager
    private GameManager gameManager;

    private void Start()
    {
        // Find the GameManager object and get its GameManager script component
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player"))
        {
            // Trigger game failure
            gameManager.FailGame();
        }
    }
}
