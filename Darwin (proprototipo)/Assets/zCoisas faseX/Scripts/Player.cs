using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    GameObject parede;
    GameObject pB;
    public float speedP = 1f; // Velocidade de movimento ajustável

    public Rigidbody2D rbplayer;
    public float speed;
    public float jumpforce;
    private SpriteRenderer sr;
    public bool Infloor = true;
    private bool encostou = false;
    private Animator animator;
    private bool isDead = false;  // Variável para controlar o estado de morte
    GameObject cameraPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rbplayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraPos = GameObject.Find("Main Camera");

        parede = GameObject.Find("parede assustadora");
        pB = GameObject.Find("ponto B");

    }

    void Update()
    {
        if (!isDead)
        {
            Jump();
            AtualizarAnimacoes();
            cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 29.5f, 149.5f), Mathf.Clamp(transform.position.y, -4, 13.7f), cameraPos.transform.position.z);
        }

        if(encostou){
          parede.transform.position = Vector3.MoveTowards(parede.transform.position, pB.transform.position, speedP * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float HM = Input.GetAxisRaw("Horizontal");
        rbplayer.velocity = new Vector2(HM * speed, rbplayer.velocity.y);

        if (HM > 0)
        {
            sr.flipX = false;
        }
        else if (HM < 0)
        {
            sr.flipX = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Infloor)
        {
            rbplayer.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            Infloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.name == "Ground" || collision.gameObject.name == "esquerda" || collision.gameObject.name == "direita")
        {
            Infloor = true;
            jumpforce = 7f;  // Certifique-se de definir o valor correto para cada caso
        }
        if (collision.gameObject.name == "Wall")
        {
            Infloor = true;
            jumpforce = 8f;
        }
        
        if (collision.gameObject.CompareTag("Armadilhas"))
        {
            if (!isDead)  // Verifica se o personagem já não está morto
            {
                isDead = true;  // Define o estado de morte
                animator.SetBool("taPulando", false);
                animator.SetTrigger("Dead");
                rbplayer.velocity = Vector2.zero;  // Para o movimento do personagem
                rbplayer.isKinematic = true;  // Torna o Rigidbody cinemático para impedir mais movimentação
                PlayerPrefs.SetString("UltimaFase", SceneManager.GetActiveScene().name);
                if(movePlayer.fasemorte){
                    movePlayer.fasemorte = false;
                    Invoke("fase", 0.5f);
                }
                else{
                    movePlayer.fasemorte = true;
                    Invoke("gameover", 0.8f);
                }
            }
        }

    }
    private void fase(){
        SceneManager.LoadScene("morte", LoadSceneMode.Single);
    }
    private void gameover(){
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("pontoM")){
            encostou = true;
        }
    }

    void AtualizarAnimacoes()
    {
        if (!isDead)  // Atualiza animações apenas se não estiver morto
        {
            animator.SetBool("taPulando", !Infloor);

            float HM = Input.GetAxisRaw("Horizontal");
            animator.SetBool("taCorrendo", HM != 0);
        }
    }
}
