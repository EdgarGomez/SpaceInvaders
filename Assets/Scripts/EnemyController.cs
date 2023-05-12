using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyHolder;
    public float speed;
    public float stepDown;

    private bool movingRight = true;
    private float enemyHalfWidth;
    private float minX;
    private float maxX;

    // New variables
    private Transform leftmostEnemy;
    private Transform rightmostEnemy;

    void Start()
    {
        // Get the half width of a child enemy's sprite
        enemyHalfWidth = enemyHolder.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.extents.x;

        // Calculate the x position boundaries
        float screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        minX = -screenHalfWidth + enemyHalfWidth;
        maxX = screenHalfWidth - enemyHalfWidth;
    }

    void Update()
    {
        UpdateExtremeEnemies();
        MoveEnemies();
    }

    // New method to update the leftmost and rightmost enemies
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
            if (enemyHolder.transform.position.x > maxX)
            {
                movingRight = false;
                enemyHolder.transform.position += Vector3.down * stepDown;
            }
        }
        else
        {
            enemyHolder.transform.position += Vector3.left * speed * Time.deltaTime;
            if (enemyHolder.transform.position.x < minX)
            {
                movingRight = true;
                enemyHolder.transform.position += Vector3.down * stepDown;
            }
        }
    }

    public void HandleBorderCollision()
    {
        movingRight = !movingRight; // Reverse direction

        // Move all enemies down by "stepDown" units
        enemyHolder.transform.position += Vector3.down * stepDown;
    }

}
