using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbplayer;
    public float speed;
    public float jumpforce;
    private SpriteRenderer sr;
    public bool Infloor = true;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rbplayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float HM = Input.GetAxisRaw("Horizontal");

        rbplayer.velocity = new Vector2(HM * speed, rbplayer.velocity.y);

        if(HM > 0)
        {
            sr.flipX = false;
        }
        else if(HM < 0)
        {
            sr.flipX = true;
        }

    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && Infloor)
        {
            rbplayer.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            Infloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            Infloor = true;
        }
    }
}
