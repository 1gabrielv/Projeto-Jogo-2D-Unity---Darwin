using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guarda : MonoBehaviour
{
    private float speedPlayer = 4.8f;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; 
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    private float raioDeVerificacao = 0.2f;
    private LayerMask layerDoChao;
    private Animator animator; 
    private GameObject cameraPos;

    [SerializeField] private GameObject playerPrefab; // Prefab do sapo
    private GameObject playerInstance; // Instância do sapo

    // ataque
    private bool isAttacking = false; // Variável para verificar se o personagem está atacando
    private int numAttack = 0;
     // hitbox de ataque
    private GameObject attackHitbox;
    private int contadorDanos = 0;


    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); 
        oSpriteRenderer = GetComponent<SpriteRenderer>(); 
        verificadorDeChao = transform.Find("verificarChao"); 
        layerDoChao = LayerMask.GetMask("chao"); 
        animator = GetComponent<Animator>();
        cameraPos = GameObject.Find("Main Camera");
        playerPrefab = Resources.Load<GameObject>("protaF3");
        // Encontrar a hitbox de ataque
        attackHitbox = transform.Find("attackGuarda").gameObject;
        
        // Certifique-se de que a hitbox está desativada no início
        attackHitbox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 76.6f, 242), Mathf.Clamp(transform.position.y, 11.5f, 11.5f), cameraPos.transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.E)) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) // Permitir ataque apenas se não estiver atacando
        {
            attack();
        }
    }

    void FixedUpdate() 
{
    if (!isAttacking) // Permitir movimento apenas se não estiver atacando
    {
        moveplayer();
    }
    else
    {
        oRigidbody2D.velocity = new Vector2(0, oRigidbody2D.velocity.y); // Impedir movimento
    }

    // Atualizar a posição da hitbox de ataque com base na direção do personagem
    if (!oSpriteRenderer.flipX)
    {
        attackHitbox.transform.localPosition = new Vector3(0f, -0.6f, 0f); // Posição quando o personagem está virado para a direita
    }
    else
    {
        attackHitbox.transform.localPosition = new Vector3(-1.9f, -0.6f, 0f); // Posição quando o personagem está virado para a esquerda
    }
}
    public void moveplayer() 
    { 
        float inputMove = Input.GetAxisRaw("Horizontal");
        oRigidbody2D.velocity = new Vector2(inputMove * speedPlayer, oRigidbody2D.velocity.y);

        if (inputMove > 0) 
        {
            oSpriteRenderer.flipX = false; 
        }
        if (inputMove < 0) 
        {
            oSpriteRenderer.flipX = true; 
        }
    }

    void AtualizarAnimacoes() 
    {
        float inputMove = Input.GetAxisRaw("Horizontal");
        animator.SetBool("taCorrendo", inputMove != 0 && !isAttacking); // Não permitir animação de corrida durante ataque
    }

    void TrocarFormPlayer()
    {
        playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void attack() 
    {
        isAttacking = true; // Definir como atacando

        if (numAttack == 0) 
        {
            animator.SetBool("taAtacando", true);
            attackHitbox.SetActive(true); // Ativar a hitbox de ataque
            StartCoroutine(ResetAttack("taAtacando"));
            numAttack = 1;
        } 
        else if (numAttack == 1) 
        {
            animator.SetBool("taAtacando 2", true);
            attackHitbox.SetActive(true); // Ativar a hitbox de ataque
            StartCoroutine(ResetAttack("taAtacando 2"));
            numAttack = 0;
        }
    }

    IEnumerator ResetAttack(string attackBool) 
    {
        yield return new WaitForSeconds(0.5f); // Tempo suficiente para a animação de ataque ser executada
        animator.SetBool(attackBool, false);
        attackHitbox.SetActive(false); // Desativar a hitbox de ataque
        isAttacking = false; // Definir como não atacando após a animação
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Armadilhas"))
        {
            if (contadorDanos == 0) // Verifica se o personagem ainda não morreu
            {
                contadorDanos++;
                animator.SetBool("dano", true);
                StartCoroutine(ResetDamageAnimation());
            }
            else if(contadorDanos == 1){
                speedPlayer = 0;
                animator.SetBool("morte", true); // Ativa animação de morte
                    StartCoroutine(DestruirPersonagem()); // Chama o método para destruir o personagem após a animação
            }   
            }
        }

    IEnumerator ResetDamageAnimation()
    {
        yield return new WaitForSeconds(0.3f); // Aguarda 0.3 segundos
        animator.SetBool("dano", false); // Desativa a animação de dano
    }

    IEnumerator DestruirPersonagem()
    {
        // Desativa o movimento e espera a animação de morte terminar
        oRigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.0f); // Aguarda 1 segundo após a animação de morte
        Destroy(gameObject); // Destrói o personagem
    }
}

