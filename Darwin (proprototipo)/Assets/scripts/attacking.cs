using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("Enemy")) // Verifica se a colisão foi com um inimigo
        {
            // Adicione aqui o código para aplicar dano ao inimigo
            collision.GetComponent<Enemy>().TakeDamage(10); // Supondo que o inimigo tenha um método TakeDamage
        }*/
    }
}
