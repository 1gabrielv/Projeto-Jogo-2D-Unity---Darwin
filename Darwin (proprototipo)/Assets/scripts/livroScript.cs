using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class livroScript : MonoBehaviour
{
    private SpriteRenderer oSpriteRenderer;
    private Rigidbody2D oRigidbody2D; 
    private float speedLivro = 6f;
    GameObject cameraPos;


    [SerializeField] private GameObject playerPrefab; // Prefab do sapo
    private GameObject playerInstance; // Instância do sapo

    private GameObject fumaça;


    // Start is called before the first frame update
    void Start()
    {
        oSpriteRenderer = GetComponent<SpriteRenderer>();
        oRigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipSpriteY());
        cameraPos = GameObject.Find("Main Camera");



        playerPrefab = Resources.Load<GameObject>("protaF3");
        fumaça = transform.Find("fumaça").gameObject;
        fumaça.SetActive(false);
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
        float inputMoveVertical = Input.GetAxisRaw("Vertical");
        
        oRigidbody2D.velocity = new Vector2(inputMoveHorizontal * speedLivro, inputMoveVertical * speedLivro);

        // Flip horizontal
        if (inputMoveHorizontal > 0) {
            oSpriteRenderer.flipX = false; // Vira o sprite para a direita
        }
        else if (inputMoveHorizontal < 0) {
            oSpriteRenderer.flipX = true; // Vira o sprite para a esquerda
        }

        // Rotate for vertical movement
        if (inputMoveVertical > 0 && (inputMoveHorizontal > 0 || inputMoveHorizontal < 0)) {
            transform.rotation = Quaternion.Euler(0, oSpriteRenderer.flipX ? 180 : 0, 45); // Rotate up
        }
        else if (inputMoveVertical < 0 && (inputMoveHorizontal < 0 || inputMoveHorizontal > 0)) {
            transform.rotation = Quaternion.Euler(0, oSpriteRenderer.flipX ? 180 : 0, -45); // Rotate down
        }
        else {
            transform.rotation = Quaternion.Euler(0, oSpriteRenderer.flipX ? 180 : 0, 0); // Default rotation
        }
    }

    private IEnumerator FlipSpriteY()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            oSpriteRenderer.flipY = !oSpriteRenderer.flipY;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Armadilhas"))
        {
            StartCoroutine(HandleTrap());
        }
    }

     private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("slime")){
            StartCoroutine(HandleTrap());
            PlayerPrefs.SetString("UltimaFase", SceneManager.GetActiveScene().name);
            if(movePlayer.fasemorte){
                movePlayer.fasemorte = false;
                Invoke("fase", 0.5f);
            }
            else{
                movePlayer.fasemorte = true;
                Invoke("gameover", 0.5f);
            }
        }
    }

    private void fase(){
        SceneManager.LoadScene("morte", LoadSceneMode.Single);
    }
    private void gameover(){
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    private IEnumerator HandleTrap()
    {
        speedLivro = 0;
        fumaça.SetActive(true);
        yield return new WaitForSeconds(1f); // Espera 1 segundo
        Destroy(gameObject);
    }
}
