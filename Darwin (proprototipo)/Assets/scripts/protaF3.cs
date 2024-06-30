using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protaF3 : MonoBehaviour
{
    public float forcaPulo = 5f;
    public float speedPlayer = 4f;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; // Parte do personagem que controla o sprite (olhar pra frente ou pra trás ou ficar de cabeça pra baixo)
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    public float raioDeVerificacao;
    private LayerMask layerDoChao;
    private Animator animator; // Troca de animações
    GameObject cameraPos;
    private bool isDead = false;  // Variável para controlar o estado de morte

    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); // Atribui o Rigidbody sem precisar arrastar dentro da Unity
        if (oRigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no GameObject.");
        }
        oSpriteRenderer = GetComponent<SpriteRenderer>(); // Atribui o SpriteRenderer sem precisar arrastar dentro da Unity
        if (oSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no GameObject.");
        }

        verificadorDeChao = transform.Find("verificarChao"); // Acha um GameObject de forma privada
        if (verificadorDeChao == null)
        {
            Debug.LogError("Verificador de chão não encontrado. Certifique-se de que 'verificarChao' é um filho de 'protaF3'.");
        }
        // verificadorDeChao = GameObject.Find("verificarChao").transform; <-- ou isso caso não dê certo o de cima
        layerDoChao = LayerMask.GetMask("chao"); // Define a LayerMask para o layer "chao"
        if (layerDoChao == 0)
        {
            Debug.LogError("Layer 'chao' não encontrado. Certifique-se de que o layer está configurado corretamente.");
        }
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator não encontrado no GameObject.");
        }

        cameraPos = GameObject.Find("Main Camera");
        if (cameraPos == null)
        {
            Debug.LogError("Main Camera não encontrada. Certifique-se de que a câmera se chama 'Main Camera'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    if (!isDead)
        {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        }
        cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 76.6f, 200), Mathf.Clamp(transform.position.y, 11.5f, 11.5f), cameraPos.transform.position.z);

    }
void FixedUpdate() { // A Unity às vezes buga sem isso pra movimentar o player

    if (!isDead)
        {
        moveplayer();
        pular();
        }
    }

    public void moveplayer() { // Fazer o player andar
        float inputMove = Input.GetAxisRaw("Horizontal");
        oRigidbody2D.velocity = new Vector2(inputMove * speedPlayer, oRigidbody2D.velocity.y);

        if (inputMove > 0) {
            oSpriteRenderer.flipX = false; // Vira o sprite para a direita
        }
        if (inputMove < 0) {
            oSpriteRenderer.flipX = true; // Vira o sprite para a esquerda
        }
    }

    public void pular() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))&& estaNoChao) {
            oRigidbody2D.velocity = Vector2.up * forcaPulo;
        }
    }

    void AtualizarAnimacoes() {
        // Atualiza a animação de pulo
        animator.SetBool("taPulando", !estaNoChao);

        // Atualiza a animação de corrida
        float inputMove = Input.GetAxisRaw("Horizontal");
        animator.SetBool("taCorrendo", inputMove != 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armadilhas"))
        {
            if (!isDead)  // Verifica se o personagem já não está morto
            {
                isDead = true;  // Define o estado de morte
                animator.SetBool("taPulando", false);
                animator.SetTrigger("Dead");
                oRigidbody2D.velocity = Vector2.zero;  // Para o movimento do personagem
                oRigidbody2D.isKinematic = true;  // Torna o Rigidbody cinemático para impedir mais movimentação
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Armadilhas")){
            isDead = true;
            animator.SetBool("taPulando", false);
            animator.SetTrigger("Dead");
            oRigidbody2D.velocity = Vector2.zero;  // Para o movimento do personagem
            oRigidbody2D.isKinematic = true;  // Torna o Rigidbody cinemático para impedir mais movimentação
        }
    }
}
