using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 3f;
    private Rigidbody2D rb;
    private bool hasFallen = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
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
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }
}
