using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private int shieldHits = 0;
    private SpriteRenderer spriteRenderer;
    private PlayerController player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            ApplyShieldDamage();
        }
    }

    public void ApplyShieldDamage()
    {
        shieldHits++;
        switch (shieldHits)
        {
            case 1:
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.66f);
                break;
            case 2:
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.33f);
                break;
            case 3:
                if (player != null)
                {
                    player.DestroyShield();
                }
                break;
        }
    }
}
