using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float height = 2f;

    private Vector2 startPos;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
    }

    void FixedUpdate()
    {
        float y = Mathf.Sin(Time.time * speed) * height;
        rb.MovePosition(startPos + Vector2.up * y);
    }
}
