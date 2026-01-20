using Unity.VisualScripting;
using UnityEngine;

public class LeftandRightPlatform : MonoBehaviour
{
    public float speed = 20f;
    public Transform player;
    public float triggerDistance = 3f;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool hasMoved = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        startPos = rb.position;
    }

    void Update()
    {
        if(hasMoved) return;
        float distanceX = Mathf.Abs(player.position.x - rb.position.x);

        if (distanceX <= triggerDistance)
        {
            float x = Mathf.Exp(Time.time * speed) ;
            rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
        }
        
        
    }
    


}
