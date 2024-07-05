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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(FlapRoutine());

        attackHitbox = transform.Find("dragaoHit").gameObject;
        attackHitbox.SetActive(false);

        Dano = transform.Find("poder dragao").gameObject;
        Dano.SetActive(false);
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
            Dano.SetActive(true);
            StartCoroutine(DisableDanoAfterDelay(2f)); // Inicia a corrotina para desativar Dano após 2 segundos
        }
    }

    IEnumerator DisableDanoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera o tempo especificado (2 segundos)
        Dano.SetActive(false); // Desativa Dano
    }
}
