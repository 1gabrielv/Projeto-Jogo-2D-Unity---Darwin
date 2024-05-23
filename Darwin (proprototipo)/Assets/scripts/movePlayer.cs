using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float forcaPulo;
    public float speedPlayer;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; // Parte do personagem que controla o sprite (olhar pra frente ou pra trás ou ficar de cabeça pra baixo)
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    public float raioDeVerificacao;
    private LayerMask layerDoChao;
    private Animator animator; // Troca de animações
    GameObject cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); // Atribui o Rigidbody sem precisar arrastar dentro da Unity
        oSpriteRenderer = GetComponent<SpriteRenderer>(); // Atribui o SpriteRenderer sem precisar arrastar dentro da Unity

        verificadorDeChao = transform.Find("verificarChao"); // Acha um GameObject de forma privada
        // verificadorDeChao = GameObject.Find("verificarChao").transform; <-- ou isso caso não dê certo o de cima
        layerDoChao = LayerMask.GetMask("chao"); // Define a LayerMask para o layer "chao"
        animator = GetComponent<Animator>();
        cameraPos = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        cameraPos.transform.position = new Vector3(cameraPos.transform.position.x, oRigidbody2D.transform.position.x, cameraPos.transform.position.z);
        cameraPos.transform.position = new Vector3(cameraPos.transform.position.y, oRigidbody2D.transform.position.y, cameraPos.transform.position.z);

    }
    
    void FixedUpdate() { // A Unity às vezes buga sem isso pra movimentar o player
        moveplayer();
        pular();
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
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao) {
            oRigidbody2D.velocity = Vector2.up * forcaPulo;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && estaNoChao) {
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
}