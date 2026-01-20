using UnityEngine;

public class FallingPillar : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 2f;

    public float torqueForce = 80f;
    public float gravity = 4f;

    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                         RigidbodyConstraints2D.FreezePositionY;
    }

    void Update()
    {
        if (hasFallen) return;

        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceX <= triggerDistance)
        {
            Fall();
        }
    }

    void Fall()
    {
        hasFallen = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = gravity;
        rb.AddTorque(torqueForce, ForceMode2D.Impulse);
    }
}
