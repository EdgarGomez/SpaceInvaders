using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject explosionPrefab;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.IncrementEnemies();
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




