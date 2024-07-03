using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esfregaoPlayer : MonoBehaviour
{
    private SpriteRenderer oSpriteRenderer;
    private Rigidbody2D oRigidbody2D; 
    private float speedLivro = 6f;
    GameObject cameraPos;
    private bool isMoving = false;

    [SerializeField] private GameObject playerPrefab; // Prefab do sapo
    private GameObject playerInstance; // Instância do sapo
    private bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        oSpriteRenderer = GetComponent<SpriteRenderer>();
        oRigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipSpriteY());
        cameraPos = GameObject.Find("Main Camera");

        playerPrefab = Resources.Load<GameObject>("protaF3");
    }

    // Update is called once per frame
    void Update()
    {
        moveplayer();
        if (Input.GetKeyDown(KeyCode.E)) // Troca de forma ao pressionar a tecla E
        {
            TrocarFormPlayer();
        }
        cameraPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 76.6f, 242), Mathf.Clamp(transform.position.y, 11.5f, 11.5f), cameraPos.transform.position.z);
    }

    public void moveplayer() 
    { 
        float inputMoveHorizontal = Input.GetAxisRaw("Horizontal");
        
        oRigidbody2D.velocity = new Vector2(inputMoveHorizontal * speedLivro, oRigidbody2D.velocity.y);


        // Verifica se o personagem está se movendo
        isMoving = inputMoveHorizontal != 0;

        // Flip horizontal
        if (inputMoveHorizontal > 0) {
            oSpriteRenderer.flipX = false; // Vira o sprite para a direita
        }
        else if (inputMoveHorizontal < 0) {
            oSpriteRenderer.flipX = true; // Vira o sprite para a esquerda
        }
    }

    private IEnumerator FlipSpriteY()
    {
        while (true)
        {
            if (isMoving)
            {
                transform.rotation = Quaternion.Euler(0, 0, -45);
                yield return new WaitForSeconds(0.38f);
                transform.rotation = Quaternion.Euler(0, 0, 45);
                yield return new WaitForSeconds(0.38f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                yield return null; // Espera até a próxima frame
            }
        }
    }

    void FixedUpdate() 
    { 
        // A Unity às vezes buga sem isso pra movimentar o player
        moveplayer();
    }

    void TrocarFormPlayer()
    {
        playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("slime")){
            Destroy(col.gameObject);
        }
    }
}
