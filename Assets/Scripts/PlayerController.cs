using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    private bool hasMissile = false;
    private int missileCount = 0;
    public float fireRate = 0.5f;

    private float nextFire = 0.0f;

    public int playerLives = 4;
    public Image[] lifeBar;
    public AudioClip collisionSound;
    public AudioClip shootSound;
    public AudioClip impactSound;
    public GameObject collisionEffectPrefab;
    private AudioSource audioSource;

    public GameObject shieldPrefab;
    private GameObject activeShield;
    public int shields = 0;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        GetComponent<Rigidbody2D>().velocity = movement * speed;


        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -2.5f, 2.5f);
        transform.position = pos;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (hasMissile && missileCount > 0)
            {
                Instantiate(missilePrefab, transform.position, Quaternion.identity);
                missileCount--;
                GameManager.instance.UpdateMissileCount(missileCount);
                if (missileCount <= 0)
                {
                    hasMissile = false;
                }
            }
            else
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Bum!");
            UpdateLifeBar(collision);
        }
    }

    public void UpdateLifeBar(Collision2D collision)
    {

        if (playerLives > 1 && playerLives <= 4)
        {
            Debug.Log("Removing lifes");

            audioSource.PlayOneShot(impactSound);
            lifeBar[playerLives - 1].enabled = false;
            playerLives--;
        }
        else
        {
            lifeBar[playerLives - 1].enabled = false;
            audioSource.PlayOneShot(collisionSound);
            SpawnCollisionEffect(collision.contacts[0].point);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(WaitGameOver());
        }
    }

    public void SpawnCollisionEffect(Vector2 position)
    {
        GameObject effectInstance = Instantiate(collisionEffectPrefab, position, Quaternion.identity);
        Destroy(effectInstance, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup != null)
        {
            HandlePickup(pickup);
            Destroy(pickup.gameObject);
        }
    }

    public void HandlePickup(Pickup pickup)
    {
        switch (pickup.type)
        {
            case Pickup.PickupType.Missile:
                hasMissile = true;
                missileCount += 2;
                GameManager.instance.UpdateMissileCount(missileCount);
                break;
            case Pickup.PickupType.Health:
                if (playerLives < 4)
                {
                    playerLives++;
                    lifeBar[playerLives - 1].enabled = true;
                }
                break;
            case Pickup.PickupType.Shield:
                shields++;
                GameManager.instance.UpdateShields(shields);
                if (activeShield == null)
                {
                    activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
                    activeShield.transform.parent = null;
                    activeShield.transform.localScale = shieldPrefab.transform.localScale;
                    activeShield.transform.parent = transform;
                }
                break;
        }
    }
    public void DestroyShield()
    {
        Destroy(activeShield);
        shields--;
        GameManager.instance.UpdateShields(shields);
        if (shields > 0)
        {
            StartCoroutine(InstantiateNewShield());
        }
    }

    private IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.GameOver();
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator InstantiateNewShield()
    {
        yield return new WaitForSeconds(0.3f);
        activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        activeShield.transform.parent = null;
        activeShield.transform.localScale = shieldPrefab.transform.localScale;
        activeShield.transform.parent = transform;
    }
}
