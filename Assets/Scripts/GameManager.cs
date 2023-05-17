using UnityEngine;
using TMPro;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text scoreText;
    public TMP_Text waveText;
    public TMP_Text enemiesText;
    public TMP_Text overScoreText;
    public TMP_Text overWaveText;
    public TMP_Text overEnemiesText;
    public TMP_Text totalText;
    public TMP_Text missileCountText;
    public TMP_Text shieldsText;
    public TMP_Text countdownText;
    public GameObject gameOverPanel;

    private float score;
    public int waves;
    private int enemies;
    private int totalScore = 0;

    private bool escPressed = false;
    public bool isPaused = false;
    public GameObject pausePanel;

    public TMP_InputField scoreInputField;
    public Button saveScoreButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        score = 0;
        waves = 1;
        enemies = 0;
    }

    void Update()
    {
        score += Time.deltaTime;
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escPressed)
            {

                isPaused = !isPaused;
                Time.timeScale = isPaused ? 0 : 1;
                pausePanel.SetActive(isPaused);
                escPressed = true;

            }
            else
            {
                escPressed = false;
            }
        }
    }

    public void PauseOn()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public IEnumerator WaveCountdown()
    {
        countdownText.gameObject.SetActive(true);
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = "Next Wave " + countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }

    public void SavePlayerScore()
    {
        string playerName = scoreInputField.text.Substring(0, Mathf.Min(3, scoreInputField.text.Length)).ToUpper();

        int playerScore = totalScore;

        DataManager.instance.SavePlayerData(playerName, playerScore);
        scoreInputField.enabled = false;
        saveScoreButton.enabled = false;
    }

    public void IncrementWaves()
    {
        waves++;
    }

    public void IncrementEnemies()
    {
        enemies++;
    }

    public void UpdateMissileCount(int count)
    {
        missileCountText.text = count.ToString();
    }

    public void UpdateShields(int shields)
    {
        shieldsText.text = shields.ToString();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString("0");
        waveText.text = waves.ToString();
        enemiesText.text = enemies.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        overScoreText.text = $"Score: {(int)score}";
        overWaveText.text = $"Waves: {waves}x20={waves * 20}";
        overEnemiesText.text = $"Enemies: {enemies}x10={enemies * 10}";

        totalScore = (int)(score + waves * 20 + enemies * 10);

        StartCoroutine(AnimateScore(totalScore));
    }

    IEnumerator AnimateScore(int totalScore)
    {
        int currentScore = 0;
        float timer = 0;

        float duration = 2.0f;

        while (currentScore < totalScore)
        {
            currentScore = (int)Mathf.Lerp(0, totalScore, timer / duration);
            totalText.text = $"Total: {currentScore}";

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        totalText.text = $"Total: {totalScore}";
    }
}
