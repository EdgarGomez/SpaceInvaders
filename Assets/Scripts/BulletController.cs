using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject explosionPrefab;
    public bool isMissile = false;
    public float explosionRadius = 1f;
    public Vector2 direction = Vector2.up;


    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.IncrementEnemies();

            if (isMissile)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }

            Instantiate(explosionPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}





