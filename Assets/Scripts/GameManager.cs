using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text scoreText;
    public TMP_Text waveText;
    public TMP_Text enemiesText;

    private float score;
    private int waves;
    private int enemies;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Initialization
        score = 0;
        waves = 1;
        enemies = 0;
    }

    void Update()
    {
        score += Time.deltaTime; // Increment score per time
        UpdateUI();
    }

    public void IncrementWaves()
    {
        waves++;
    }

    public void IncrementEnemies()
    {
        enemies++;
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString("0");
        waveText.text = "Waves: " + waves.ToString();
        enemiesText.text = enemies.ToString();
    }

    // Call this when the player is destroyed or game over condition is met
    public void GameOver()
    {
        // Game over logic
    }
}
