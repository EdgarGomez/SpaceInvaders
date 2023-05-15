using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Missile,
        Health,
        Shield
    }

    public PickupType type;
    public float speed = 1f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
