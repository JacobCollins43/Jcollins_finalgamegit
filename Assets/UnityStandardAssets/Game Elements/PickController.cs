using UnityEngine;

public class PickController : MonoBehaviour
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
            // Notify the GameManager that a pick has been collected
            gameManager.CollectPick();

            // Destroy the pick object
            Destroy(gameObject);
        }
    }
}
