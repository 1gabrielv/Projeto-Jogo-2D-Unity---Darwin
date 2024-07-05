using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protaf4 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedP = 1f; // Velocidade de movimento ajustável

    public Rigidbody2D rbplayer;
    public float speed;
    public float jumpforce;
    private SpriteRenderer sr;
    public bool Infloor = true;
    private Animator animator;
    private bool isDead = false;  // Variável para controlar o estado de morte
    GameObject cameraPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rbplayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraPos = GameObject.Find("Main Camera");

    }

    void Update()
    {
        if (!isDead)
        {
            Jump();
            AtualizarAnimacoes();
            cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.43f, 3.5f), Mathf.Clamp(transform.position.y, -2, 59.4f), cameraPos.transform.position.z);
        }

    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float HM = Input.GetAxisRaw("Horizontal");
        rbplayer.velocity = new Vector2(HM * speed, rbplayer.velocity.y);

        if (HM > 0)
        {
            sr.flipX = false;
        }
        else if (HM < 0)
        {
            sr.flipX = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Infloor)
        {
            rbplayer.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            Infloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.name == "chaocave")
        {
            Infloor = true;
        }
        
        if (collision.gameObject.CompareTag("Armadilhas"))
        {
            if (!isDead)  // Verifica se o personagem já não está morto
            {
                isDead = true;  // Define o estado de morte
                animator.SetBool("taPulando", false);
                animator.SetTrigger("Dead");
                rbplayer.velocity = Vector2.zero;  // Para o movimento do personagem
                rbplayer.isKinematic = true;  // Torna o Rigidbody cinemático para impedir mais movimentação
            }
        }

    }

    void AtualizarAnimacoes()
    {
        if (!isDead)  // Atualiza animações apenas se não estiver morto
        {
            animator.SetBool("taPulando", !Infloor);

            float HM = Input.GetAxisRaw("Horizontal");
            animator.SetBool("taCorrendo", HM != 0);
        }
    }
}
