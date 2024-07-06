using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class livro2 : MonoBehaviour
{
    private List<Transform> pontos; // Lista de pontos a serem seguidos
    private Rigidbody2D rb;
    private Transform currentPoint;
    private float speed = 6.8f;
    private SpriteRenderer oSpriteRenderer;
    private int currentPointIndex = 0; // Índice do ponto atual

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oSpriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializa a lista de pontos
        pontos = new List<Transform>
        {
            GameObject.Find("p1 livro2").transform,
            GameObject.Find("p2 livro2").transform,
            GameObject.Find("p3 livro2").transform,
            GameObject.Find("p4 livro2").transform
        };

        // Define o ponto inicial
        currentPoint = pontos[currentPointIndex];
        StartCoroutine(FlipSpriteY());
    }

    void Update()
    {
        MoveTowardsCurrentPoint();
    }

    void MoveTowardsCurrentPoint()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.velocity = direction * speed;

        if (direction.x > 0)
        {
            oSpriteRenderer.flipX = false; // Vira o sprite para a direita
        }
        else if (direction.x < 0)
        {
            oSpriteRenderer.flipX = true; // Vira o sprite para a esquerda
        }

        // Verifica se chegou ao ponto atual
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            // Atualiza o índice do ponto atual para o próximo ponto
            currentPointIndex = (currentPointIndex + 1) % pontos.Count;
            currentPoint = pontos[currentPointIndex];
        }

        // Rotaciona para o movimento vertical
        if (currentPointIndex == 2)
        {
            transform.rotation = Quaternion.Euler(0, oSpriteRenderer.flipX ? 180 : 0, 45); // Rotaciona para cima
        }
        else if (currentPointIndex == 3)
        {
            transform.rotation = Quaternion.Euler(0, oSpriteRenderer.flipX ? 180 : 0, -45); // Rotaciona para baixo
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, oSpriteRenderer.flipX ? 180 : 0, 0); // Rotação padrão
        }
    }
    private IEnumerator FlipSpriteY()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            oSpriteRenderer.flipY = !oSpriteRenderer.flipY;
        }
    }

}
