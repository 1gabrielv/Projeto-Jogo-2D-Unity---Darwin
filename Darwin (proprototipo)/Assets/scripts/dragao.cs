using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragao : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    private float pauseTimer = 0f; // Temporizador para a pausa
    private bool isPaused = false; // Indica se o caracol está pausado
    private bool isFlapping = false; // Indica se a animação de batendo está ativa
    private float flapInterval = 2.1f; // Intervalo de tempo para a animação de batendo


    private GameObject attackHitbox;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(FlapRoutine());

        attackHitbox = transform.Find("dragaoHit").gameObject;
        attackHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlapRoutine()
    {
        while (true)
        {
            animator.SetBool("batendo", true); // Ativa a animação de batendo
            yield return new WaitForSeconds(flapInterval); // Espera o tempo da animação de batendo
            animator.SetBool("batendo", false); // Desativa a animação de batendo
            yield return new WaitForSeconds(flapInterval); // Espera o intervalo antes de repetir
        }
    }
}
