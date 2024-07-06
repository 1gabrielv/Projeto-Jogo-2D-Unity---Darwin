using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estrela : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 direction;

    private float minX = -8.3f;
    private float maxX = 8.3f;
    private float minY = -4.2f;
    private float maxY = 4.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
        rb.velocity = direction * speed;
    }

    void Update()
    {
        Vector2 position = transform.position;

        // Check horizontal bounds (X axis)
        if (position.x <= minX && direction.x < 0)
        {
            direction.x = -direction.x;
            rb.velocity = direction * speed;
        }
        else if (position.x >= maxX && direction.x > 0)
        {
            direction.x = -direction.x;
            rb.velocity = direction * speed;
        }

        // Check vertical bounds (Y axis)
        if (position.y <= minY && direction.y < 0)
        {
            direction.y = -direction.y;
            rb.velocity = direction * speed;
        }
        else if (position.y >= maxY && direction.y > 0)
        {
            direction.y = -direction.y;
            rb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Destroy(gameObject);
        }
    }
}