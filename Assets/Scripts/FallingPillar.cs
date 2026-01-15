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

        // SADECE pozisyonu kilitle, rotation SERBEST
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

        // Kilitleri tamamen kaldýr
        rb.constraints = RigidbodyConstraints2D.None;

        // Yerçekimi aç
        rb.gravityScale = gravity;

        // Z ekseninde + yönde devrilme
        rb.AddTorque(torqueForce, ForceMode2D.Impulse);
    }
}
