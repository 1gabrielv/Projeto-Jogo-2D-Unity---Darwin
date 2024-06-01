using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sapo : MonoBehaviour
{
    GameObject pontoA;
    GameObject pontoB;
    Rigidbody2D rb;
    Animator animator;
    Transform currentPoint;
    public float speed = 1.3f;
    public float pauseDuration = 0.8f; // Duração da pausa em segundos
    private SpriteRenderer oSpriteRenderer;
    private float pauseTimer = 0f; // Temporizador para a pausa
    private bool isPaused = false; // Indica se o caracol está pausado


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pontoA = GameObject.Find("pa sapo");
        pontoB = GameObject.Find("pb sapo");
        animator = GetComponent<Animator>();
        animator.SetBool("taCorrendo", true);
        currentPoint = pontoB.transform;
        oSpriteRenderer = GetComponent<SpriteRenderer>();
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
            Vector2 ponto = currentPoint.position - transform.position;
            if (currentPoint == pontoB.transform)
            {
                rb.velocity = new Vector2(speed, 0); // faz ele seguir até o pontoB
                oSpriteRenderer.flipX = true;
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0); // inverte o lado que ele anda para ir para o pontoA
                oSpriteRenderer.flipX = false;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                if (currentPoint == pontoB.transform)
                {
                    currentPoint = pontoA.transform; // transforma a posição do current para o A para que ele volte para o B depois
                }
                else
                {
                    currentPoint = pontoB.transform; // transforma a posição do current para o B para que ele volte para o A depois
                }

                // Inicia a pausa
                isPaused = true;
                pauseTimer = pauseDuration;
                animator.SetBool("taCorrendo", false);
                rb.velocity = Vector2.zero; // para o movimento do caracol durante a pausa
            }
        }
    }
}