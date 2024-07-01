using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
 
    private float forcaPulo = 7f;
    public float speedPlayer = 4f;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; // Parte do personagem que controla o sprite (olhar pra frente ou pra trás ou ficar de cabeça pra baixo)
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    public float raioDeVerificacao;
    private LayerMask layerDoChao;
    private Animator animator; // Troca de animações
    GameObject cameraPos;

    //para a transformação do sapo
    [SerializeField] private GameObject sapoPrefab; // Prefab do sapo
    private GameObject sapoInstance; // Instância do sapo
    private bool isSapo = false;

    //para a transformação do caracol
    [SerializeField] private GameObject caracolPrefab; // Prefab do sapo
    private GameObject caracolInstance; // Instância do sapo
    private bool isCaracol = false;

    //para a transformação do gato
    [SerializeField] private GameObject gatoPrefab; // Prefab do sapo
    private GameObject gatoInstance; // Instância do sapo
    private bool isGato = false;

    //para a transformação do javali
    [SerializeField] private GameObject javaliPrefab; // Prefab do sapo
    private GameObject javaliInstance; // Instância do sapo
    private bool isJavali = false;
    

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
        sapoPrefab = Resources.Load<GameObject>("sapoplayer");
        gatoPrefab = Resources.Load<GameObject>("gatoplayer");
        caracolPrefab = Resources.Load<GameObject>("caracolplayer");
        javaliPrefab = Resources.Load<GameObject>("javaliplayer");

    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -114, 282), Mathf.Clamp(transform.position.y, -4.3f, 5.2f), cameraPos.transform.position.z);

        if (Input.GetKeyDown(KeyCode.E) && isSapo == true) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormaSapo();
        }

        if (Input.GetKeyDown(KeyCode.E) && isGato == true) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormaGato();
        }

        if (Input.GetKeyDown(KeyCode.E) && isCaracol == true) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormaCaracol();
        }

        if (Input.GetKeyDown(KeyCode.E) && isJavali == true) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormaJavali();
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
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Sapo"))
    {
        isSapo = true;
    }
    else
    {
        isSapo = false;
    }

    if (other.CompareTag("caracol"))
    {
        isCaracol = true;
    }

    else
    {
        isCaracol = false;
    }
    if (other.CompareTag("gato"))
    {
        isGato = true;
    }
    else
    {
        isGato = false;
    }

    if (other.CompareTag("javali"))
    {
        isJavali = true;
    }

    else
    {
        isJavali = false;
    }
}



    void TrocarFormaSapo()
    {
 
        // Instancia o novo prefab
        sapoInstance = Instantiate(sapoPrefab, transform.position, transform.rotation);
        // Destrói o personagem atual
        Destroy(gameObject);

    }

    void TrocarFormaGato()
    {
 
        // Instancia o novo prefab
        gatoInstance = Instantiate(gatoPrefab, transform.position, transform.rotation);
        // Destrói o personagem atual
        Destroy(gameObject);

    }

    void TrocarFormaCaracol()
    {
 
        // Instancia o novo prefab
        caracolInstance = Instantiate(caracolPrefab, transform.position, transform.rotation);
        // Destrói o personagem atual
        Destroy(gameObject);

    }

    void TrocarFormaJavali()
    {
 
        // Instancia o novo prefab
        javaliInstance = Instantiate(javaliPrefab, transform.position, transform.rotation);
        // Destrói o personagem atual
        Destroy(gameObject);

    }

}