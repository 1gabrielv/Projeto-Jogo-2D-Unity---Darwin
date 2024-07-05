using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esqueletao2 : MonoBehaviour
{
   GameObject pontoA;
    GameObject pontoB;
    Rigidbody2D rb;
    Animator animator;
    Transform currentPoint;
    private float speed = 2f;
    private float pauseDuration = 1f; // Duração da pausa em segundos
    private SpriteRenderer oSpriteRenderer;
    private float pauseTimer = 0f; // Temporizador para a pausa
    private bool isPaused = false; // Indica se o caracol está pausado

    private GameObject attackHitbox;
    public bool isAttacking = false;
    public bool isDano = false;
    public bool stun = false;

    // Duração da animação de morte em segundos
    private float deathAnimationDuration = 1.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pontoA = GameObject.Find("pa esq2");
        pontoB = GameObject.Find("pb esq2");
        animator = GetComponent<Animator>();
        animator.SetBool("taCorrendo", true);
        currentPoint = pontoB.transform;
        oSpriteRenderer = GetComponent<SpriteRenderer>();

        attackHitbox = transform.Find("attackEsq2").gameObject;
        attackHitbox.SetActive(false);
    }

    void Update()
    {
        if (isPaused)
        {
            // Se o caracol está pausado, diminui o temporizador
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                // Termina a pausa
                isPaused = false;
                animator.SetBool("taCorrendo", true);
            }
        }
        else
        {
            Vector2 direction = (currentPoint.position - transform.position).normalized;

            // Movimenta o esqueleto na direção do ponto atual
            rb.velocity = direction * speed;

            // Atualiza a orientação do sprite
            oSpriteRenderer.flipX = direction.x < 0;

            // Define a posição do hitbox de ataque com base na direção do movimento
            if (direction.x > 0)
            {
                attackHitbox.transform.localPosition = new Vector2(6f, 0.5f); // Posição à direita
            }
            else
            {
                attackHitbox.transform.localPosition = new Vector2(2f, 0.5f); // Posição à esquerda
            }

            // Verifica se o esqueleto chegou próximo ao ponto atual e inverte o ponto de destino
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                if (currentPoint == pontoB.transform)
                {
                    currentPoint = pontoA.transform; // Troca para o ponto A
                }
                else
                {
                    currentPoint = pontoB.transform; // Troca para o ponto B
                }

                // Inicia a pausa
                isPaused = true;
                pauseTimer = pauseDuration;
                animator.SetBool("taCorrendo", false);
                rb.velocity = Vector2.zero; // Para o movimento do esqueleto durante a pausa
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("attack"))
        {
            speed = 0;
            animator.SetBool("taStun", false);
            animator.SetBool("taAtacando", false);
            animator.SetBool("taCorrendo", false);
            animator.SetBool("dano", true);
            stun = true;
            StartCoroutine(ResetAfterStun());
        }
    }

    private IEnumerator ResetAfterStun()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Ativando animação de morte");
        animator.SetBool("dano", false); // Desativa a animação de dano
        animator.SetBool("morte", true);
        yield return new WaitForSeconds(deathAnimationDuration); // Espera a duração da animação de morte
        Debug.Log("Destruindo o cogumelo");
        Destroy(gameObject); // Destroi o cogumelo
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("player"))
        {
            if (!stun)
            {
                if (speed == 2)
                {
                    animator.SetBool("taAtacando", true);
                    speed = 0;
                    StartCoroutine(WaitAndReset());
                    StartCoroutine(WaitAndReset2());
                    StartCoroutine(WaitAndReset3());
                }
            }
        }
    }

    private IEnumerator WaitAndReset()
    {
        // Espera por 0.4 segundos
        yield return new WaitForSeconds(0.4f);
        attackHitbox.SetActive(true);
    }

    private IEnumerator WaitAndReset2()
    {
        // Espera por 1 segundo
        yield return new WaitForSeconds(1f);
        animator.SetBool("taAtacando", false);
        attackHitbox.SetActive(false);
    }

    private IEnumerator WaitAndReset3()
    {
        // Espera por 2.5 segundos
        yield return new WaitForSeconds(2.5f);
        speed = 2;
        animator.SetBool("taCorrendo", true);
    }
}

