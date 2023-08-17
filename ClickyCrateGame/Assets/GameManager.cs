using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;

    private float spawnRate = 1;

    public bool isGameActive;
    void Start()
    {
       
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        Debug.Log("Game Over");
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleText.gameObject.SetActive(false);
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
