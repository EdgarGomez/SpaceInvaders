using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyController enemyController;

    private void Start()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            enemyController.HandleBorderCollision();
        }
    }
}
