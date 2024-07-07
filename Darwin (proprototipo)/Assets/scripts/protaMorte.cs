using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class protaMorte : MonoBehaviour
{
    private float forcaPulo = 5f;
    private float speedPlayer = 3f;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; // Parte do personagem que controla o sprite (olhar pra frente ou pra trás ou ficar de cabeça pra baixo)
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    public float raioDeVerificacao;
    private LayerMask layerDoChao;
    private Animator animator; // Troca de animações
    private int contEstrelas = 0;
    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); // Atribui o Rigidbody sem precisar arrastar dentro da Unity
        oSpriteRenderer = GetComponent<SpriteRenderer>(); // Atribui o SpriteRenderer sem precisar arrastar dentro da Unity
        verificadorDeChao = transform.Find("verificarChao"); // Acha um GameObject de forma privada
        // verificadorDeChao = GameObject.Find("verificarChao").transform; <-- ou isso caso não dê certo o de cima
        layerDoChao = LayerMask.GetMask("chao"); // Define a LayerMask para o layer "chao"
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
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
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("star"))
    {
        contEstrelas = contEstrelas + 1;
        Destroy(other);
        if(contEstrelas == 9){
            Invoke("retorna", 0.5f);
        }
    }
}
    private void retorna(){
        string ultimaFase = PlayerPrefs.GetString("UltimaFase", "Fase 1");
        SceneManager.LoadScene(ultimaFase, LoadSceneMode.Single);
    }

}