using System.Collections;
using UnityEngine;

public class dragao : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    private float flapInterval = 2.1f; // Intervalo de tempo para a animação de batendo

    private GameObject Dano;

    private GameObject attackHitbox;
    int cont = 2;

    private GameObject Morte;
    private int hits = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(FlapRoutine());

        attackHitbox = transform.Find("dragaoHit").gameObject;
        attackHitbox.SetActive(false);

        Dano = transform.Find("poder dragao").gameObject;
        Dano.SetActive(false);

        Morte = transform.Find("morte").gameObject;
        Morte.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cont == 1)
        {
            attackHitbox.SetActive(true);
        }
        else if (cont == 0)
        {
            attackHitbox.SetActive(false);
        }
    }

    IEnumerator FlapRoutine()
    {
        while (true)
        {
            animator.SetBool("batendo", true); // Ativa a animação de batendo
            cont--;
            yield return new WaitForSeconds(1.75f); // Espera o tempo da animação de batendo
            animator.SetBool("batendo", false); // Desativa a animação de batendo
            cont--;
            yield return new WaitForSeconds(flapInterval); // Espera o intervalo antes de repetir
            cont = 2;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("attack"))
        {
            if (hits < 5)
            {
                hits++;
                Dano.SetActive(true);
                StartCoroutine(DisableDanoAfterDelay(1.4f)); // Inicia a corrotina para desativar Dano após 1.4 segundos
            }
            else
            {
                Morte.SetActive(true);
                StopAllCoroutines(); // Para todas as corrotinas em execução
                animator.enabled = false; // Desativa todas as animações
                rb.velocity = Vector2.zero; // Para qualquer movimento
                StartCoroutine(DestroyAfterDelay(2.8f)); // Inicia a corrotina para destruir o GameObject após 3 segundos
            }
        }
    }

    IEnumerator DisableDanoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera o tempo especificado (1.4 segundos)
        Dano.SetActive(false); // Desativa Dano
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera o tempo especificado (3 segundos)
        Destroy(gameObject); // Destroi o GameObject
    }
}
