using UnityEngine;

public class SpikesTrigger : MonoBehaviour
{
    public float speed = 5f;
    public float moveHeight = 1.5f;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 targetPos;

    private enum State { Idle, Up, Down }
    private State state = State.Idle;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;

        startPos = rb.position;
        targetPos = startPos + Vector2.up * moveHeight;
    }

    void FixedUpdate()
    {
        if (state == State.Up)
            MoveUp();
        else if (state == State.Down)
            MoveDown();
    }

    public void Trigger()
    {
        if (state == State.Idle)
            state = State.Up;
    }

    void MoveUp()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, targetPos) < 0.01f)
            state = State.Down;
    }

    void MoveDown()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, startPos, speed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, startPos) < 0.01f)
            state = State.Idle;
    }
}
