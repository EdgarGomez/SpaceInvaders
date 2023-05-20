using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController enemyController;
    [SerializeField] protected GameObject turboFireLeft;
    [SerializeField] protected GameObject turboFireRight;
    [SerializeField] protected GameObject enemyBulletPrefab;
    [SerializeField] protected AudioSource audioSource;
    public AudioClip shootSound;

    public float bulletSpeed = 5f;

    void Start()
    {
        turboFireLeft.SetActive(false);
        turboFireRight.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, enemyBulletPrefab.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
        audioSource.PlayOneShot(shootSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            enemyController.HandleBorderCollision();
        }
        else if (other.gameObject.CompareTag("EnemyLimit"))
        {
            StartCoroutine(WaitGameOver());
        }
    }

    private IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.GameOver();
    }

    public void ShowTurboFire()
    {
        turboFireLeft.SetActive(true);
        turboFireRight.SetActive(true);
    }

    public void HideTurboFire()
    {
        turboFireLeft.SetActive(false);
        turboFireRight.SetActive(false);

    }
}
