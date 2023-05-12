using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.5f;
    private Vector3 startPosition;
    private float quadWidth;

    void Start()
    {
        startPosition = transform.position;
        quadWidth = GetComponent<Renderer>().bounds.size.y;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, quadWidth);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
