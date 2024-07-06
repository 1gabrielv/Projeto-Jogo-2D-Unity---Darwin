using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapoPlayer : MonoBehaviour
{
    public float forcaPulo = 12f;
    public float speedPlayer = 3f;
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

    private GameObject fumaça;

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

        fumaça = transform.Find("fumaça").gameObject;
        fumaça.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -114, 300), Mathf.Clamp(transform.position.y, -4.3f, 5.2f), cameraPos.transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.E)) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormaPlayer();
        }
    }
    
    void FixedUpdate() 
    {
        moveplayer();
        pular();
    }

    public void moveplayer() 
    { 
        float inputMove = Input.GetAxisRaw("Horizontal");
        oRigidbody2D.velocity = new Vector2(inputMove * speedPlayer, oRigidbody2D.velocity.y);

        if (estaNoChao)
        {
            if (inputMove < 0) 
            {
                oSpriteRenderer.flipX = false; 
            }
            else if (inputMove > 0) 
            {
                oSpriteRenderer.flipX = true; 
            }
        }
    }

    public void pular() 
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && estaNoChao) 
        {
            oRigidbody2D.velocity = Vector2.up * forcaPulo;
        }
    }

    void AtualizarAnimacoes() 
    {
        animator.SetBool("taPulando", !estaNoChao);
        float inputMove = Input.GetAxisRaw("Horizontal");
        animator.SetBool("taCorrendo", inputMove != 0);
        
        if (!estaNoChao)
        {
            if (inputMove > 0) 
            {
                oSpriteRenderer.flipX = false; 
            }
            else if (inputMove < 0) 
            {
                oSpriteRenderer.flipX = true; 
            }
        }
    }

    void TrocarFormaPlayer()
    {
        playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Armadilhas"))
        {
            StartCoroutine(HandleTrap());
        }
    }

    private IEnumerator HandleTrap()
    {
        speedPlayer = 0;
        fumaça.SetActive(true);
        yield return new WaitForSeconds(1f); // Espera 1 segundo
        Destroy(gameObject);
    }
}
