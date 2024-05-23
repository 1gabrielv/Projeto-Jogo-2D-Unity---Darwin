using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
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

    public GameObject sapoPrefab; // Prefab do sapo
    private GameObject sapoInstance; // Instância do sapo
    private bool isSapo = false;
    

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

        if (Input.GetKeyDown(KeyCode.E)) // Troca de forma ao pressionar a tecla E
        {
            TrocarForma();
        }
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

    void TrocarForma()
    {
        isSapo = !isSapo; // Inverte o estado da forma

        if (isSapo) // Se estiver na forma de sapo
        {
            // Desativa o script e o sprite do jogador principal
            GetComponent<movePlayer>().enabled = false;
            oSpriteRenderer.enabled = false;

            // Instancia o sapo e guarda a referência
            sapoInstance = Instantiate(sapoPrefab, transform.position, transform.rotation);
        }
        else // Se estiver na forma de jogador principal
        {
            // Destroi a instância do sapo
            if (sapoInstance != null)
            {
                Destroy(sapoInstance);
            }

            // Ativa o script e o sprite do jogador principal
            GetComponent<movePlayer>().enabled = true;
            oSpriteRenderer.enabled = true;
        }
    }

}