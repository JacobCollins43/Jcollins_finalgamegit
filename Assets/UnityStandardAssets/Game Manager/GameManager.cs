using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;

    private float elapsedTime = 0;
    private bool isRunning = false;
    private bool isFinished = false;

    internal void FinishedGame()
    {
        throw new NotImplementedException();
    }

    private int pickCount = 0;
    private int tier = 0;

    private FirstPersonController fpsController;

    public void Start()
    {
        Physics.autoSyncTransforms = true;
        fpsController = player.GetComponent<FirstPersonController>();
        fpsController.enabled = false;
    }

    public void StartGame()
    {
        elapsedTime = 0;
        isRunning = true;
        isFinished = false;
        pickCount = 0;
        tier = 0;

        PositionPlayer();
        fpsController.enabled = true;
    }

    public void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 45)
            {
                FailGame();
            }
        }
    }

    public void PositionPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }

    public void CollectPick()
    {
        pickCount++;
    }

    public void DeductPick()
    {
        pickCount--;
        if (pickCount < 0)
        {
            pickCount = 0;
        }
    }

    public void FailGame()
    {
        isRunning = false;
        isFinished = true;
        fpsController.enabled = false;

        // Determine the tier based on the pick count
        if (pickCount >= 8)
        {
            tier = 3;
        }
        else if (pickCount >= 4)
        {
            tier = 2;
        }
        else if (pickCount >= 2)
        {
            tier = 1;
        }
        else
        {
            tier = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnGUI()
    {
        if (!isRunning)
        {
            string message = isFinished ? "Click or Press Enter to Play Again" : "Click or Press Enter to Play";
            Rect startButton = new Rect(Screen.width / 2 - 120, Screen.height / 2, 240, 30);

            if (GUI.Button(startButton, message) || Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }

        if (isFinished)
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "Your Time Was");
            GUI.Label(new Rect(Screen.width / 2 - 10, 200, 20, 30), ((int)elapsedTime).ToString());

            GUI.Box(new Rect(Screen.width / 2 - 65, 225, 130, 40), "Your Pick Count");
            GUI.Label(new Rect(Screen.width / 2 - 10, 240, 20, 30), pickCount.ToString());

            GUI.Box(new Rect(Screen.width / 2 - 65, 265, 130, 40), "Tier Reached");
            GUI.Label(new Rect(Screen.width / 2 - 10, 280, 20, 30), tier.ToString());

            string restartButtonLabel = isFinished ? "Play Again" : "Restart";
            Rect restartButton = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 40, 240, 30);

            if (GUI.Button(restartButton, restartButtonLabel))
            {
                RestartGame();
            }

            Rect quitButton = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 80, 240, 30);

            if (GUI.Button(quitButton, "Quit"))
            {
                QuitGame();
            }
        }
        else if (isRunning)
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Time Remaining");
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 20, 30), ((int)(45 - elapsedTime)).ToString());

            GUI.Box(new Rect(Screen.width / 2 - 65, 10, 130, 40), "Pick Count");
            GUI.Label(new Rect(Screen.width / 2 - 10, 25, 20, 30), pickCount.ToString());
        }
    }
}

