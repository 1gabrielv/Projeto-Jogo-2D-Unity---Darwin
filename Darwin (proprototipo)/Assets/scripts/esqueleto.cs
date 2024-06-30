using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esqueleto : MonoBehaviour
{
    GameObject pontoA;
    GameObject pontoB;
    Rigidbody2D rb;
    Animator animator;
    Transform currentPoint;
    public float speed = 2f;
    public float pauseDuration = 2.2f; // Duração da pausa em segundos
    private SpriteRenderer oSpriteRenderer;
    private float pauseTimer = 0f; // Temporizador para a pausa
    private bool isPaused = false; // Indica se o esqueleto está pausado

    // Ataques
    private GameObject attackHitbox;
    public bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pontoA = GameObject.Find("pa esqueleto");
        pontoB = GameObject.Find("pb esqueleto");
        animator = GetComponent<Animator>();
        animator.SetBool("taCorrendo", true);
        currentPoint = pontoB.transform;
        oSpriteRenderer = GetComponent<SpriteRenderer>();

        attackHitbox = transform.Find("attackEsq").gameObject;
        attackHitbox.SetActive(false);
    }

    void Update()
    {

        if (isPaused)
        {
            // Se o esqueleto está pausado, diminui o temporizador
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
                attackHitbox.transform.localPosition = new Vector2(0.4f, 1f); // Posição à direita
            }
            else
            {
                attackHitbox.transform.localPosition = new Vector2(-1f, 1f); // Posição à esquerda
            }

            // Ativa o ataque quando colide com o personagem
            /*if (isAttacking)
            {
                attackHitbox.SetActive(true);
            }
            else
            {
                attackHitbox.SetActive(false);
            }*/

            

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


    // void OnTriggerEnter2D(Collider2D col){
    //     if (col.gameObject.CompareTag("player"))
    //     {
    //         attackHitbox.SetActive(true);
    //         animator.SetBool("taAtacando", true);
    //         speed = 0;
    //         Debug.Log("Player collided with enemy!");
    //         // Aqui você pode adicionar lógica para causar dano ao jogador, por exemplo.
    //     }
    //     else{
    //         attackHitbox.SetActive(false);
    //         animator.SetBool("taAtacando", false);
    //         speed = 2f;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("player"))
        {
            animator.SetBool("taAtacando", true);
            speed = 0;
            StartCoroutine(WaitAndReset());

            
            StartCoroutine(WaitAndReset2());


            StartCoroutine(WaitAndReset3());

    }
}

private IEnumerator WaitAndReset()
{
    // Espera por 1 segundo
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
    // Espera por 1 segundo
    yield return new WaitForSeconds(2.5f);
    speed = 2;
    }
    
}

