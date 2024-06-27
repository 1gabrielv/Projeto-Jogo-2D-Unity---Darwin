using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPlayer : MonoBehaviour
{
    public float speedPlayer = 3.8f;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; 
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    public float raioDeVerificacao;
    private LayerMask layerDoChao;
    private Animator animator; 
    private GameObject cameraPos;

    [SerializeField] private GameObject playerPrefab; // Prefab do sapo
    private GameObject playerInstance; // Instância do sapo
    private bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); 
        oSpriteRenderer = GetComponent<SpriteRenderer>(); 
        verificadorDeChao = transform.Find("verificarChao"); 
        layerDoChao = LayerMask.GetMask("chao"); 
        animator = GetComponent<Animator>();
        cameraPos = GameObject.Find("Main Camera");
        playerPrefab = Resources.Load<GameObject>("prota");
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -114, 300), Mathf.Clamp(transform.position.y, -4.3f, 5.2f), cameraPos.transform.position.z);
        if (Input.GetKeyDown(KeyCode.E)) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormPlayer();
        }
    }

    void FixedUpdate() 
        {
            moveplayer();
        }

    public void moveplayer() 
    { 
        float inputMove = Input.GetAxisRaw("Horizontal");
        oRigidbody2D.velocity = new Vector2(inputMove * speedPlayer, oRigidbody2D.velocity.y);

        if (inputMove > 0) 
        {
            oSpriteRenderer.flipX = true; 
        }
        if (inputMove < 0) 
        {
            oSpriteRenderer.flipX = false; 
        }
    }

    void AtualizarAnimacoes() 
    {
        float inputMove = Input.GetAxisRaw("Horizontal");
        animator.SetBool("taCorrendo", inputMove != 0);
    }
    void TrocarFormPlayer()
    {
            playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
    }
}

