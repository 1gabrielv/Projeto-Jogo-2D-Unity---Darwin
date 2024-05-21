using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float forcaPulo;
    public float speedPlayer;
    Rigidbody2D oRigidbody2D; 
    SpriteRenderer oSpriteRenderer; //parte do persinagem que controla o sprite(olhar pra fentre ou pra tras ou ficar de cabeça pra baixo)
    bool estaNoChao;
    Transform verificadorDeChao;
    public float raioDeVerificacao;
    LayerMask layerDoChao;
    // Start is called before the first frame update
    void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>(); //atribui o rigidbody sem precisar arrastar dentro da unity (isso semrpre dar erro)
        oSpriteRenderer = GetComponent<SpriteRenderer>(); // Atribui o SpriteRenderer sem precisar arrastar dentro da Unity

        verificadorDeChao = transform.Find("verificarChao"); //acha um GameObject de forma privada
        //verificadorDeChao = GameObject.Find("verificarChao").transform; <-- ou isso caso não de certo o de cima
        layerDoChao = LayerMask.GetMask("chao"); // Define a LayerMask para o layer "chao" 


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate() { //a unity as vezes buga sem isso pra movimentar o player
        moveplayer();
        pular();
    }
    public void moveplayer(){ //fazer o player andar
        float inputMove = Input.GetAxisRaw("Horizontal");
        oRigidbody2D.velocity = new Vector2(inputMove * speedPlayer, oRigidbody2D.velocity.y);

        if(inputMove > 0){
            oSpriteRenderer.flipX = false; // Vira o sprite para a direita
        }
        if(inputMove < 0){
            oSpriteRenderer.flipX = true; // Vira o sprite para a esquerda
        }
    }
    public void pular(){
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);
        if(Input.GetKeyDown(KeyCode.Space) && estaNoChao == true){
            oRigidbody2D.velocity = Vector2.up * forcaPulo;
        }
    }
}
