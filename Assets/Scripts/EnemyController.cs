using System.Collections;
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

    void Start()
    {
        float screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        minX = -screenHalfWidth + enemyHalfWidth;
        maxX = screenHalfWidth - enemyHalfWidth;
        SpawnEnemies();
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
        yield return new WaitForSeconds(waveCooldown);
        speed += difficultyIncrease;
        stepDown += difficultyIncrease;
        // Reset position of enemyHolder
        enemyHolder.transform.position = new Vector3(0, 4, 0);
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

}
