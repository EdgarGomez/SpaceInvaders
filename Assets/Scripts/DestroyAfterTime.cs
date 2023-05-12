using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 0.6f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
