using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapoPlayer : MonoBehaviour
{
    public float forcaPulo = 8f;
    public float speedPlayer = 3f;
    private Rigidbody2D oRigidbody2D; 
    private SpriteRenderer oSpriteRenderer; 
    private bool estaNoChao;
    private Transform verificadorDeChao; 
    public float raioDeVerificacao;
    private LayerMask layerDoChao;
    private Animator animator; 
    private GameObject cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); 
        oSpriteRenderer = GetComponent<SpriteRenderer>(); 
        verificadorDeChao = transform.Find("verificarChao"); 
        layerDoChao = LayerMask.GetMask("chao"); 
        animator = GetComponent<Animator>();
        cameraPos = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        AtualizarAnimacoes();
        cameraPos.transform.position = new Vector3(transform.position.x, transform.position.y, cameraPos.transform.position.z);
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

        if (inputMove > 0) 
        {
            oSpriteRenderer.flipX = false; 
        }
        if (inputMove < 0) 
        {
            oSpriteRenderer.flipX = true; 
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
    }
}