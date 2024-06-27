using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class livroScript : MonoBehaviour
{
    private SpriteRenderer oSpriteRenderer;
    private Rigidbody2D oRigidbody2D; 
    public float speedLivro = 6f;

    // Start is called before the first frame update
    void Start()
    {
        oSpriteRenderer = GetComponent<SpriteRenderer>();
        oRigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipSpriteY());
    }

    // Update is called once per frame
    void Update()
    {
        moveplayer();
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
            yield return new WaitForSeconds(1f);
            oSpriteRenderer.flipY = !oSpriteRenderer.flipY;
        }
    }

    void FixedUpdate() 
    { 
        // A Unity às vezes buga sem isso pra movimentar o player
        moveplayer();
    }
}
