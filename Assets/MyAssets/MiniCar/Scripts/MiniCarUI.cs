using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MiniCarUI : MonoBehaviour
{
    public GameStateManager gameStateManager; // Set in inspector
    public GameObject timerTextObj; // Set in inspector
    public GameObject packageCountTextObj; // Set in inspector
    public GameObject AnnouncementTextObj; // Set in inspector
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI packageCountText;
    private TextMeshProUGUI AnnouncementText;

    private int packageCount = 0;
    private int timeLeft;

    // Awake method, used mainly for initialization before start method
    private void Awake()
    {
        timerText = timerTextObj.GetComponent<TextMeshProUGUI>();
        packageCountText = packageCountTextObj.GetComponent<TextMeshProUGUI>();
        AnnouncementText = AnnouncementTextObj.GetComponent<TextMeshProUGUI>();
        timerTextObj.SetActive(false);
    }

    // Start method, used for when the scene starts
    private void Start()
    {
        ResetText();        
    }

    public void Update()
    {

    }

    // MainMenu button event
    public void returnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    // StartGame button event
    public void StartGame()
    {
        if (GameStateManager.gameState.Equals(GameStateManager.GameState.notPlaying))
        {
            gameStateManager.SetGameState(GameStateManager.GameState.playing);
            gameStateManager.SetPackagesCollected(0);
            ResetText();
            StartCoroutine(StartTimer(timeLeft));
        }
        
    }

    // Coroutine to run the game timer alongside the game
    public IEnumerator StartTimer(int timeLeft)
    {
        timerTextObj.SetActive(true);
        double currentTime = timeLeft;
        // Run timer until time goes to 0
        while (currentTime >= 0)
        {
            timerText.text = "Time Left: " + TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        // After timer finishes
        AnnouncementTextObj.SetActive(true);
        AnnouncementText.text = "You managed to collect " + packageCount + " presents! Good Job!";
        gameStateManager.SetGameState(GameStateManager.GameState.notPlaying);

        // TODO maybe: After a delay, reset the present count and remove the timer.
    }

    // Resets the UI text back to default
    private void ResetText()
    {
        timeLeft = 60;
        packageCount = 0;
        timerText.text = "Time Left: 60:00";
        packageCountText.text = "Presents Collected: 0";
        AnnouncementText.text = "";
    }
    
    public void setPackageCount(int packagesCollected)
    {
        packageCount = packagesCollected;
        packageCountText.text = "Presents Collected: " + packageCount;
    }
}
