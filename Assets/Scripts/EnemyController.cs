using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyHolder;
    public float speed;
    public float stepDown;
    public float waveCooldown = 3f;
    public float difficultyIncrease = 0.1f;

    private bool movingRight = true;
    private float enemyHalfWidth;
    private float minX;
    private float maxX;

    private Transform leftmostEnemy;
    private Transform rightmostEnemy;
    private bool preparingNewWave = false;

    private float shootingInterval = 1f;

    void Start()
    {
        float screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        minX = -screenHalfWidth + enemyHalfWidth;
        maxX = screenHalfWidth - enemyHalfWidth;
        SpawnEnemies();
        StartCoroutine(ShootingRoutine());
    }

    void Update()
    {
        if (enemyHolder.transform.childCount > 0)
        {
            UpdateExtremeEnemies();
            MoveEnemies();
        }
        else if (!preparingNewWave)
        {
            preparingNewWave = true;
            StartCoroutine(StartNextWave());
        }
    }

    private void UpdateExtremeEnemies()
    {
        leftmostEnemy = rightmostEnemy = enemyHolder.transform.GetChild(0);

        foreach (Transform child in enemyHolder.transform)
        {
            if (child.position.x < leftmostEnemy.position.x)
            {
                leftmostEnemy = child;
            }

            if (child.position.x > rightmostEnemy.position.x)
            {
                rightmostEnemy = child;
            }
        }
    }
    private void MoveEnemies()
    {
        if (movingRight)
        {
            enemyHolder.transform.position += Vector3.right * speed * Time.deltaTime;
            if (rightmostEnemy.position.x > maxX)
            {
                movingRight = false;
                StartCoroutine(SmoothStepDown());
            }
        }
        else
        {
            enemyHolder.transform.position += Vector3.left * speed * Time.deltaTime;
            if (leftmostEnemy.position.x < minX)
            {
                movingRight = true;
                StartCoroutine(SmoothStepDown());
            }
        }
    }

    IEnumerator SmoothStepDown()
    {
        Vector3 startPosition = enemyHolder.transform.position;
        Vector3 targetPosition = startPosition + Vector3.down * stepDown;
        float elapsedTime = 0f;
        float transitionTime = 1f;

        foreach (Transform child in enemyHolder.transform)
        {
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ShowTurboFire();
            }
        }

        while (elapsedTime < transitionTime)
        {
            enemyHolder.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemyHolder.transform.position = targetPosition;

        foreach (Transform child in enemyHolder.transform)
        {
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.HideTurboFire();
            }
        }
    }

    public void HandleBorderCollision()
    {
        movingRight = !movingRight;
        enemyHolder.transform.position += Vector3.down * stepDown;
    }

    IEnumerator StartNextWave()
    {
        GameManager.instance.IncrementWaves();
        StartCoroutine(GameManager.instance.WaveCountdown());
        yield return new WaitForSeconds(waveCooldown);
        speed += difficultyIncrease;
        stepDown += difficultyIncrease;
        // Reset position of enemyHolder
        enemyHolder.transform.position = new Vector3(0, 4, 0);
        shootingInterval = Mathf.Max(0.2f, shootingInterval - 0.1f); // Make sure shootingInterval doesn't go below 0.2
        SpawnEnemies();
        preparingNewWave = false; // Reset preparingNewWave to false after spawning enemies
    }

    void SpawnEnemies()
    {
        int lines = Random.Range(3, 5);
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, enemyPrefab.transform.rotation, enemyHolder.transform);
                enemy.transform.localPosition = new Vector3(-2 + j, -i, 0); // Set local position relative to enemyHolder
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.enemyController = this;
                if (i == 0 && j == 0) // If it's the first enemy
                {
                    // Get the half width of the enemy's sprite
                    enemyHalfWidth = enemy.GetComponent<SpriteRenderer>().bounds.extents.x;
                }
            }
        }
    }

    private List<Enemy> GetShootingEnemies()
    {
        List<Enemy> shootingEnemies = new List<Enemy>();
        for (int i = 0; i < 5; i++) // For each column of enemies
        {
            Enemy lowestEnemyInColumn = null;
            foreach (Transform child in enemyHolder.transform)
            {
                if ((int)child.position.x == i - 2 && (lowestEnemyInColumn == null || child.position.y < lowestEnemyInColumn.transform.position.y))
                {
                    lowestEnemyInColumn = child.GetComponent<Enemy>();
                }
            }
            if (lowestEnemyInColumn != null)
            {
                shootingEnemies.Add(lowestEnemyInColumn);
            }
        }
        return shootingEnemies;
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            List<Enemy> shootingEnemies = GetShootingEnemies();
            if (shootingEnemies.Count > 0)
            {
                Enemy shootingEnemy = shootingEnemies[Random.Range(0, shootingEnemies.Count)];
                shootingEnemy.Shoot();
            }
            yield return new WaitForSeconds(shootingInterval + Random.Range(-0.2f, 0.2f)); // Add some randomness to the shooting interval
        }
    }

}
