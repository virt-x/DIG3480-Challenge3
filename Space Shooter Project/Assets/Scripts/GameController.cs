using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnDelay, startDelay, waveDelay;
    public Text scoreText, restartText, gameOverText;
    private int score;
    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);
        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if (gameOver)
                {
                    break;
                }
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazards[Random.Range(0,hazards.Length)], spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnDelay);
            }
            yield return new WaitForSeconds(waveDelay);
        }
        restartText.gameObject.SetActive(true);
        yield break;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
    }
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        if (score > 99)
        {
            Win();
        }
    }

    public void Win()
    {
        if (gameOver == true)
        {
            return;
        }
        gameOver = true;
        gameOverText.text = "You win!\nGame created by Xavier Virt.";
        gameOverText.gameObject.SetActive(true);
    }
    void UpdateScoreText()
    {
        scoreText.text = "Points: " + score;
    }
}
