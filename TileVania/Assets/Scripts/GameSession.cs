using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public IEnumerator ProcessPlayerDeath()
    {
        yield return new WaitForSecondsRealtime(2);
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void addToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().RestScenePesist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
